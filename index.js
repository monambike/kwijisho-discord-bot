// VARIÁVEIS
// Tradução
var translationJS = require("./storage/translationFile.json");
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
// Info
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
// Commands
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

bot.on("ready", function(name){
	console.log("Obrigada! Agora estou viva e atualizadaa turururu");

	bot.user.setActivity("Ayaya!! Digite '!' para me chamar :D"); 
});

bot.on("message", function(msg){
	let args = msg.content.substring(PREFIX.length).split(" ");
	var validLanguage = args[1] === "pt" || args[1] === "en" || args[1] === "es" || args[1] === "jp";



	if(msg.content.startsWith("!")){
		switch(args[0]){
			case "":
				msg.channel.send("Oii " + msg.author.username + "! Tudo bem?  Eu sou a KWiJisho (KawaiiJisho). Se quiser saber o que eu sei fazer, digite !commands.");
				break;
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