#version 330 core
out vec4 FragColor;

in vec3 Normal;
in vec3 Position;

uniform vec3 cameraPos; 
uniform mat4 viewObj;

uniform samplerCube skybox;


void main()
{   
	const float Eta = 0.66; // Ratio of indices of refraction
	const float FresnelPower = 2.0;
	const float F = ((1.0-Eta) * (1.0-Eta)) / ((1.0+Eta) * (1.0+Eta));
	
	
	vec3 View = normalize (cameraPos);
	vec3 Incident = normalize (Position - cameraPos);
	vec3 Norm = normalize (Normal);
	
	vec3 Reflect = reflect(Incident,normalize (Normal));
	vec3 reflectColor = texture (skybox, Reflect).rgb;
	
	float Ratio = F + (1.0 - F) * pow((1.0 - dot(-Incident, Norm)), FresnelPower);
	
	vec3 Refract = refract(Incident,Norm,Ratio);
	vec3 refractColor = texture(skybox, Refract).rgb;

	float refractiveFactor = dot (View, Norm);
	refractiveFactor = pow(refractiveFactor, FresnelPower);
		
	vec3 environmentColor = mix( refractColor, reflectColor, refractiveFactor); 
		
		
	FragColor = vec4((environmentColor).rgb, 1.0);
	
}
