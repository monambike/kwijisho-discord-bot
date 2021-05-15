# KWiJisho
(última atualização: 15/05/2021)

## Como Editar o Bot

#### Aviso: a explicação a seguir conterá explicações de como se cria um bot, simplesmente terá informações do que é necessário para pegar e editar o meu bot.

Após realizar download dos arquivos realize os seguintes passos:

1. Crie uma pasta chamada `storage`.

2. Dentro dessa pasta crie dois arquivos com os nomes `dictionaryFile.json` e `metaData.json` e coloque **dentro dos dois arquivos** o seguinte código:

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

3. Fora dessa pasta, na raiz, inclua um arquivo com o nome `config.json` com o seguinte código:
```
{
	"prefix": "!",
	"token": "here-goes-the-token",
	"activity": "Ayaya!! Digite '!' para me chamar :D"
}
```
Você vai precisar mudar o campo token com o token do bot que você criar. Você pode mudar o prefix e a activity do bot como desejar.