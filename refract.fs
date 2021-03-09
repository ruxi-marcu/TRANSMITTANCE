#version 330 core
out vec4 FragColor;

in vec3 Normal;
in vec3 Position;

uniform vec3 cameraPos;
uniform mat4 viewObj;

uniform samplerCube skybox;

void main()
{    
	
	vec3 Incident = normalize (Position - cameraPos);
	vec3 Norm = normalize (Normal);
	
	float ratio = 1.0 /1.52;
	vec3 Refracted = refract (Incident, Norm, ratio);
	
	FragColor = texture (skybox, Refracted);
}