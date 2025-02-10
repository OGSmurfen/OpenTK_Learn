#version 330 core

in vec2 texPos;

out vec4 FragColor;

uniform sampler2D tex0;

void main()
{
	//FragColor = vec4(0.63f, 0.61f, 0.03f, 1f);
	FragColor = texture(tex0, texPos);
}