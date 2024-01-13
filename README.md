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