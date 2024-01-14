# KWiJisho

Fun and friendly interactive Discord bot with a wide range of functions, including a custom dictionary feature. KWIJisho stands for Kawaii and Jisho (Cute and Dictionary in japanese respectively).

![Captura de tela 2023-05-31 222206](https://github.com/monambike/kwijisho-discord-bot/assets/35270174/0def11c4-8d71-4b3e-b961-b55439bfecf2)

You can check the old version of this project written with Node.js (Javascript) [here](https://github.com/monambike/kwijisho-discord-bot-legacy).

## How to Edit the Bot

#### Notice: The following explanation will contain instructions on how to create a bot. It will simply provide information on what is needed to fetch and edit my bot.

After downloading the files, follow these steps:

1. Create a folder called `storage`.

2. Within this folder, create two files named `dictionaryFile.json` and `metaData.json`, and **place the following code inside both files**:

`storage\dictionaryFile.json`
```
{
    "Words": {
        "0": {
            "word": "",
            "desc": ""
        }
    }
}
```

`storage\metaData.json`
```
{
    "countOfWords": 0
}
```

3. Outside of this folder, at the root level, include a file named `config.json` with the following code:
```
{
	"prefix": "!",
	"token": "here-goes-the-token",
	"activity": "Ayaya!! Digite '!' para me chamar :D"
}
```
You will need to change the "token" field with the token of the bot you create. You can modify the "prefix" and "activity" of the bot as desired.

# Funcionalities

## Change Discord Server Theme

![Captura de tela 2024-01-14 201128](https://github.com/monambike/kwijisho-discord-bot/assets/35270174/2bc401bd-13a8-405e-bbcb-b1194d755cec)
![Captura de tela 2024-01-14 201121](https://github.com/monambike/kwijisho-discord-bot/assets/35270174/e955db72-3a40-4b99-bc14-cdd98a8a1a2b)
![Captura de tela 2024-01-14 201116](https://github.com/monambike/kwijisho-discord-bot/assets/35270174/bbe5be95-0688-439b-8ede-4d20fd7459fb)
![Captura de tela 2024-01-14 201109](https://github.com/monambike/kwijisho-discord-bot/assets/35270174/d5e1df56-8239-4e9a-8528-0d277891a043)
