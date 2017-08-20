Shader "Unlit/Unlit ScreenSpace" {
Properties {
 
    _MainTex ("Base (RGB)", 2D) = "white" {}
 
   
}
 
SubShader {
    Tags { "RenderType"="Opaque" "Queue" = "Geometry"}
   
CGPROGRAM
 
 
#pragma surface surf NoLighting  noambient
 
    sampler2D _MainTex;
 
    struct Input {
        half2 uv_MainTex;
   		float4 screenPos;
    };
         
     fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
     {
        fixed4 c;
        c.rgb = s.Albedo * 0.5;
        c.a = s.Alpha;
        return c;
     }
 
    void surf (Input IN, inout SurfaceOutput o)
    {  

		half2 screenUV = IN.screenPos.xy / IN.screenPos.w;
		fixed4 sstc = tex2D(_MainTex, screenUV);
		o.Albedo =  sstc.rgb;// + 0.5f;
       
    }
ENDCG
}
 
Fallback "Mobile/VertexLit"
}