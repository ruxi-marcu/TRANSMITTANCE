#version 330 core
out vec4 FragColor;

in vec3 Normal;
in vec3 Position;

uniform vec3 cameraPos; 
uniform mat4 viewObj;

uniform samplerCube skybox;

const vec3 IoR = vec3(0.58, 0.57, 0.62);

void main()
{    
	
	vec3 ViewDir = normalize(cameraPos);
	vec3 Incident = normalize(Position- cameraPos);
              
    vec3 RefractR = refract(ViewDir, normalize(-Normal), IoR.x);
    vec3 RefractG = refract(ViewDir, normalize(-Normal), IoR.y);
    vec3 RefractB = refract(ViewDir, normalize(-Normal), IoR.z);
			  
	vec3 Reflection = reflect(Incident, normalize(Normal));
	vec3 reflectionColor = texture(skybox, Reflection).xyz;
              
    vec3 abberColor;
    abberColor.r = vec3(texture(skybox, RefractR)).r;
    abberColor.g = vec3(texture(skybox, RefractG)).g;
    abberColor.b = vec3(texture(skybox, RefractB)).b;
              
	FragColor = vec4(mix(reflectionColor, abberColor, 0.6), 1.0);
}