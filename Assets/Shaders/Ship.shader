Shader "Custom/Ship" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Color", Color) = (0,0,0,0)
		_Phase ("Animation phase", Float) = 5
		_Period ("Animation period", Float) = 5
		_Amp ("Animation amplitude", float) = 0.002
		
	}
   SubShader {
      Tags { "Queue" = "Transparent" } 
         // draw after all opaque geometry has been drawn
      Pass {
         ZWrite Off // don't write to depth buffer 
            // in order not to occlude other objects
 
         Blend SrcAlpha OneMinusSrcAlpha // use alpha blending
 			
         CGPROGRAM 
 
         #pragma vertex vert 
         #pragma fragment frag
 
 		uniform sampler2D _MainTex;	
 		uniform float _Phase;
 		uniform float _Period;
 		uniform float _Amp;
 		uniform float4 _Color;
 		
 		struct vertexInput {
            float4 vertex : POSITION;
            float4 texcoord : TEXCOORD0;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 tex : TEXCOORD0;
         };
         
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 
            output.tex = input.texcoord;

            output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR 
         {
         	float2 tex;
         	float2 basePhase = _Phase+_Period*input.tex.xy;
         	tex.x = input.tex.x+_Amp*sin(basePhase.x)+0.001f*sin(2*basePhase.y);
         	tex.y = input.tex.y+_Amp*sin(basePhase.y)+0.001f*sin(2*basePhase.x);
            float4 col = tex2D(_MainTex, tex);
            col.xyz+=_Color.xyz;
            col.a*=_Color.a;
            //float4 alpha = tex2D(_MainTex, input.tex.xy);
            
            //col.a = alpha.a;
            
            //col = col*0.5f;
            
            return col;
               
         }
 
         ENDCG  
      }
   }
}