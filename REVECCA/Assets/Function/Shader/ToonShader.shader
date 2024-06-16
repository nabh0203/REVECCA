Shader "Custom/ToonShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "red" {}

        _BumpMap ("normal",2D) = "bump" {}
        _BumpPower ( "Normal Power", float) = 1

        [HDR]
        _RimCol ("Rim Color", Color) = (1,1,1,1)
        _RimPow ("Rim Power", float) = 2

        [HDR]
        _SpecCol ("Specular Color", Color) = (1,1,1,1)
        _SpecPow ("Specular Power", float) = 50

        [HDR]
        _FakeSpecCol ("Fake Specular Color", Color) = (1,1,1,1)
        _FakeSpecPow ("Fake Specular Power", float) = 50

        [HDR]
        _OutLineColor ("Outline Color", Color) = (1,1,1,1)
        _OutLineRange("Outline Range", float) = 1

        _CeilPower("Ceil Power", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Geometry"}
        LOD 200

        Cull Front
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert
        #pragma target 3.0

        sampler2D _MainTex;

        float4 _OutLineColor;
        float _OutLineRange;

        void vert(inout appdata_full v)
        {
           v.vertex.xyz = v.vertex.xyz + v.normal * _OutLineRange;
        }

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
            float3 lightDir;
        };

        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Emission = _OutLineColor;
        }
        ENDCG

        Cull Back
        CGPROGRAM

        #pragma surface surf Lambert
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;

        float4 _RimCol;
        float4 _SpecCol;
        float4 _FakeSpecCol;

        float _BumpPower;
        float _RimPow;
        float _SpecPow;
        float _FakeSpecPow;
        float _CeilPower;


        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
            float3 lightDir;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
         
            //Normal
            fixed3 n = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
            n = float3(n.r * _BumpPower, n.g*_BumpPower, n.b);
            o.Normal = normalize(n);

            //Rim light
            float rim = saturate(pow(1-dot(o.Normal, IN.viewDir), _RimPow));

            //Final
            //o.Emission = ((c.a *0.5)+ rim * _RimCol.rgb);
            o.Emission = rim * _RimCol.rgb * 0.5;
            o.Albedo = c.rgb;
            o.Alpha = c.a;  
        }

        float4 LightingSimple(SurfaceOutput s, float3 lightDir, float viewDir, float atten)
        {
            float NdotL = saturate(dot(s.Normal, lightDir));

            //Toon Shading
            NdotL = pow(saturate((ceil(NdotL * _CeilPower)/ _CeilPower)*0.5 + 0.5),5);

            //Specular
            float3 h = normalize(viewDir + lightDir);
            float spec = saturate(dot(s.Normal, h));
            spec = saturate(pow(spec, _SpecPow));
            spec = ceil(spec) / 5;

            //Final
            float4 finalColor;
            finalColor.rgb = s.Albedo*(NdotL + spec * _SpecCol.rgb);
            finalColor.a = s.Alpha;

            return finalColor;
        }
        ENDCG
    }
    FallBack "Diffuse"
}