// VARIÁVEIS
// Tradução
var translationJSON = `{
	"pt":{
		"lang": "Eu agora falo português!",
		"site": "Conheça o meu desenvolvedor: https://github.com/monambike",
		"hey":{
			"1": "eai! Espera... você disse '",
			"2": "', agora pouco?"
		},
		"info":{
			"title": "Meu Github!",
			"description": "Visite meu GitHub para ver mais projetos como esse!"
		},
		"commands":{
			"title": "COMANDOS",
			"description": "Essa são as coisinhas que sei fazer! Não esqueça de colocar '!' antes de comando hein!",
			"fields":{
				"nameLang": "lang (idioma)",
				"valueLang": "Escolhe um idioma. Os idiomas que eu sei falar são português (pt), inglês (en), espanhol (es) e japonês (jp).",

				"nameSite": "site",
				"valueSite": "Mostro o meu site.",

				"nameHey": "hey (mensagem)",
				"valueHey": "Teste e descubra! C:",

				"nameInfo": "info",
				"valueInfo": "Mostro algumas informações.",

				"nameCommands": "commands",
				"valueCommands": "Abro a lista de comandos."
			}
		},
		"default": "Putz cara... ;~; que vergonha... Desculpa mas não sei fazer isso... Se quiser saber o que eu sei fazer digite '!commands'."
	},

	"en":{
		"lang": "Now I speak english!",
		"site": "Know my developer: https://github.com/monambike",
		"hey":{
			"1": "hey! Hold a second... you just said '",
			"2": "', right now?"
		},
		"info":{
			"title": "Meu Github!",
			"description": "Visite meu GitHub para ver mais projetos como esse!"
		},
		"commands":{
			"title": "COMANDOS",
			"description": "Essa são as coisinhas que sei fazer! Não esqueça de colocar '!' antes de comando hein!",
			"fields":{
				"nameLang": "lang (idioma)",
				"valueLang": "Escolhe um idioma. Os idiomas que eu sei falar são português (pt), inglês (en), espanhol (es) e japonês (jp).",

				"nameSite": "site",
				"valueSite": "Mostro o meu site.",

				"nameHey": "hey (mensagem)",
				"valueHey": "Teste e descubra! C:",

				"nameInfo": "info",
				"valueInfo": "Mostro algumas informações.",

				"nameCommands": "commands",
				"valueCommands": "Abro a lista de comandos."
			}
		},
		"default": "Damn bro... ;~; that's embarrassing... Sorry but I can't do this... If you want to know what I can do type '!commands'."
	},

	"es":{
		"lang": "Yo ahora hablo español!",
		"site": "Conozca mi desarrollador: https://github.com/monambike",
		"hey":{
			"1": "Oye! Un momento... dihiste '",
			"2": "', ahora mismo?"
		},
		"info":{
			"title": "Meu Github!",
			"description": "Visite meu GitHub para ver mais projetos como esse!"
		},
		"commands":{
			"title": "COMANDOS",
			"description": "Essa são as coisinhas que sei fazer! Não esqueça de colocar '!' antes de comando hein!",
			"fields":{
				"nameLang": "lang (idioma)",
				"valueLang": "Escolhe um idioma. Os idiomas que eu sei falar são português (pt), inglês (en), espanhol (es) e japonês (jp).",

				"nameSite": "site",
				"valueSite": "Mostro o meu site.",

				"nameHey": "hey (mensagem)",
				"valueHey": "Teste e descubra! C:",

				"nameInfo": "info",
				"valueInfo": "Mostro algumas informações.",

				"nameCommands": "commands",
				"valueCommands": "Abro a lista de comandos."
			}
		},
		"default": "Ay... ;~; que vergüenza... Disculpa pero no se como hacerlo... Se quieres saber lo que se hacer escribe '!commands'."
	},

	"jp":{
		"lang": "今日本語を話せる！",
		"site": "開発者： https://github.com/monambike",
		"hey":{
			"1": "よ! ちょっと。。。 ちょうど今 「",
			"2": "」を言ったですか?"
		},
		"info":{
			"title": "Meu Github!",
			"description": "Visite meu GitHub para ver mais projetos como esse!"
		},
		"commands":{
			"title": "COMANDOS",
			"description": "Essa são as coisinhas que sei fazer! Não esqueça de colocar '!' antes de comando hein!",
			"fields":{
				"nameLang": "lang (idioma)",
				"valueLang": "Escolhe um idioma. Os idiomas que eu sei falar são português (pt), inglês (en), espanhol (es) e japonês (jp).",

				"nameSite": "site",
				"valueSite": "Mostro o meu site.",

				"nameHey": "hey (mensagem)",
				"valueHey": "Teste e descubra! C:",

				"nameInfo": "info",
				"valueInfo": "Mostro algumas informações.",

				"nameCommands": "commands",
				"valueCommands": "Abro a lista de comandos."
			}
		},
		"default": "残念。。。 ;~; 恥ずかしいね。。。 ごめんけど、　これをどうするのを分からない。。。どうするのを分かりたい、　「！commands」 をタイプしてください。"
	}
}`;
var translationJS = JSON.parse(translationJSON);
var botLang = "pt";
// Contador
var i = 0;
// BOT
const Discord = require("discord.js");
const bot = new Discord.Client();
const token = "NzM3NTM1ODQ4MTAyMzYzMjU5.Xx-xyA.ALivCZ6TyjvekWlZ5tSoLzlFW2o";
var PREFIX = "!";
// Storing Data
var fs = require("fs");
var dictionaryFile = require("./storage/dictionaryFile.json");
var substringFiltering = 0;
// Layout
var name = "Vinícius Gabriel";
var fullName = "Vinícius Gabriel Marques de Melo";
var GitHub = "https://github.com/monambike";
var color = "#8C1EFF";

bot.on("ready", function(name){
	console.log("Obrigada! Agora estou viva e atualizadaa turururu");
});

bot.on("message", function(msg){
	let args = msg.content.substring(PREFIX.length).split(" ");
	var validLanguage = args[1] === "pt" || args[1] === "en" || args[1] === "es" || args[1] === "jp";


	const info = new Discord.MessageEmbed()
		.setColor(color)
		.setTitle(translationJS[botLang]["info"]["title"])
		.setURL(GitHub)
		.setAuthor(name, 'https://i.imgur.com/wSTFkRM.png', 'https://discord.js.org')
		.setDescription(translationJS[botLang]["info"]["description"])
		.attachFiles(['resources/v-icon.png'])
		.setThumbnail('attachment://resources/v-icon.png')
		.setTimestamp()
		.setFooter(fullName + ' ('+ name +')');

	const commands = new Discord.MessageEmbed()
		.setColor(color)
		.setTitle(translationJS[botLang]["commands"]["title"])
		.setDescription("Essa são as coisinhas que sei fazer! Não esqueça de colocar '!' antes de comando hein!")
		.addFields(
			{ name: translationJS[botLang]["commands"]["fields"]["nameLang"], value: translationJS[botLang]["commands"]["fields"]["valueLang"] },
			{ name: translationJS[botLang]["commands"]["fields"]["nameSite"], value: translationJS[botLang]["commands"]["fields"]["valueSite"] },
			{ name: translationJS[botLang]["commands"]["fields"]["nameHey"], value: translationJS[botLang]["commands"]["fields"]["valueHey"] },
			{ name: translationJS[botLang]["commands"]["fields"]["nameInfo"], value: translationJS[botLang]["commands"]["fields"]["valueInfo"] },
			{ name: translationJS[botLang]["commands"]["fields"]["nameCommands"], value: translationJS[botLang]["commands"]["fields"]["valueCommands"] },
		);

	if(msg.content.startsWith("!")){
		switch(args[0]){
			case "lang":
				if(validLanguage){
					msg.channel.send(translationJS[args[1]]["lang"]);
					botLang = args[1];
					return;
				}else{
					msg.channel.send("Poxa... eu ainda não sei falar '" + msg.content.substring(6) + "' ainda...");
				}
				break;
			case "site":
				msg.channel.send(translationJS[botLang]["site"]);
				break;
			case "hey":
				msg.reply(translationJS[botLang]["hey"][1] + msg.content.substring(5) + translationJS[botLang]["hey"][2]);
				break;
			case "info":
				msg.channel.send(info);
				break;
			case "commands":
				msg.channel.send(commands);
				break;
			// DICTIONARY
			case "dictionary":
				break;
			case "addw":
				// Removing !addw, title and spaces, just letting the description;
				substringFiltering = 6 + args[1].length + 1;

				dictionaryFile[args[1]] = {
					word: args[1],
					description: msg.content.substring(substringFiltering)
				}

				fs.writeFile("./storage/dictionaryFile.json", JSON.stringify(dictionaryFile, null, 4), function(err){
					if(err){
						console.error(err);
						msg.reply("Ops... Não consegui enviar a mensagem, tenta de novo depois, oukai? ;)");
						return;
					}else{
						msg.reply("valeu, tá anotado! 📝 Gostei dessa palavra... " + JSON.stringify(args[1]) + "...");
					}
				});
				break;
			case "seew":
				try{
					const showWord = new Discord.MessageEmbed()
						.setColor(color)
						.setTitle(dictionaryFile[args[1]].word.toUpperCase())
						.setDescription(dictionaryFile[args[1]].description.toLowerCase());
					msg.channel.send(showWord);
				}catch(e){
					msg.channel.send("Putz... Desculpa mas não consegui achar essa palavra, que tal criar ela? Digite !addw (palavra) (descrição)");
				}
				break;
			case "editw":
				// Removing !editw, previous word, new word and spaces
				substringFiltering = 7 + args[1].length + 1 + args[2].length + 1;

				try{
					// Deleting old word
					delete dictionaryFile[args[1]];

					dictionaryFile[args[2]] = {
						word: args[2],
						description: msg.content.substring(substringFiltering)
					}

					fs.writeFile("./storage/dictionaryFile.json", JSON.stringify(dictionaryFile, null, 4), function(err){
						if(err){
							console.error(err);
							msg.reply("Ops... Não consegui enviar a mensagem, tenta de novo depois, oukai? ;)");
							return;
						}else{
							msg.channel.send("A palavra '" + JSON.stringify(args[1]) + "' foi atualizada!");
						}
					});
				}catch(e){
					msg.channel.send("Eh... Então, não achei essa palvra que você quer editar, que tal criar ela? Digite !addw (palavra) (descrição)");					
					console.log(e);
				}
				break;
			case "remw":
				delete dictionaryFile[args[1]];
				message.channel.send("A palavra '" + args[1] + "' foi apagada com sucesso!");
				break;
			default:
				msg.channel.send(translationJS[botLang]["default"]);
				break;
		}
	}
})

bot.login(token);