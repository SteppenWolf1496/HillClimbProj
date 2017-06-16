Shader "Mobile/Diffuse-alpha" {
    Properties{
        _AlphaCol("Alpha Color", Color) = (1,1,1,1)
        _MainTex("Base (RGB)", 2D) = "white" {}
    }
        SubShader{
        //"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"
        Tags{ "Queue" = "Transparent"
        "IgnoreProjector" = "True"
        "RenderType" = "Transparent" }
        //Cull Back
        //ZWrite On
        ZTest Lequal

        Cull Off
        Lighting Off
        ZWrite Off
        Fog{ Mode Off }

        //Cull Back
        Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
    #pragma surface surf Lambert  alphatest:alpha 
        //alpha: fade alphatest:alpha noforwardadd Lambert

        sampler2D _MainTex;
    fixed4 _AlphaCol;

    struct Input {
        float2 uv_MainTex;
    };

    void surf(Input IN, inout SurfaceOutput o) {
        fixed4 c = tex2D(_MainTex, IN.uv_MainTex);

        o.Albedo = c.rgb;
        o.Alpha = _AlphaCol.a * c.a;
    }
    ENDCG
    }
}