# XML Catcher

## Sobre

Aplicação em console com um "instalador" acoplado para configurar e executar uma tarefa do Windows a fim de se fazer backup de arquivos (especificamente XML) em um servidor FTP

## Tecnologias

O aplicativo é divido em duas partes. O "instalador" nada mais permite o fácil acesso ao arquivo XML de configurações, onde usuário, senha, pasta monitorada e outras configurações podem ser editadas, além de permitir a configuração da tarefa do Windows (Scheduled Task) que executa a segunda parte a cada 4h.

A segunda parte coleta os arquivos XML na(s) pasta(s) monitorada(s), compacta em zip e envia para um servidor FTP definido nas configurações.
