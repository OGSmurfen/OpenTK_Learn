#version 330 core

in vec2 texturePosition;

out vec4 FragColor;

uniform sampler2D tex0;
uniform sampler2D tex1;

void main()
{
	//FragColor = vec4(0.9, 0.39, 0.12, 1);
	//FragColor = texture(tex0, texturePosition);
	FragColor = mix(texture(tex0, texturePosition), texture(tex1, texturePosition), 0.2);
}