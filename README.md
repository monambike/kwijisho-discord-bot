# KWiJisho

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
