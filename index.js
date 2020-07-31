// VARIÁVEIS
// Caminhos
const dictionaryFilePath = "./storage/dictionaryFile.json";
const translationJSPath = "./storage/translationFile.json";
const metaDataPath = "./storage/metaData.json";
// Tradução
const translationJS = require(translationJSPath);
var botLang = "pt";
// Contador
var i = 0;
// BOT
const Discord = require("discord.js");
const bot = new Discord.Client();
const { prefix, token, activity } = require("./config.json");
// Storing Data
const fs = require("fs");
const dictionaryFile = require(dictionaryFilePath);
const { countOfWords } = require(metaDataPath);
var substringFiltering = 0;
var deletedWord = 0;
var page = 0;
// Counting Words
function countOfWordsUpdate(){
	fs.writeFile(metaDataPath, JSON.stringify(countOfWords, null, 4), function(err){
		if(err){
			console.error(err);
			msg.reply("Ops... Houve um problema na contagem...");
			return;
		}
	});
}
// Layout
var name = "Vinícius Gabriel";
var fullName = "Vinícius Gabriel Marques de Melo";
var GitHub = "https://github.com/monambike";
var color = "#8C1EFF";
// Info
const infoLayout = new Discord.MessageEmbed()
	.setColor(color)
	.setTitle(translationJS[botLang]["info"]["title"])
	.setURL(GitHub)
	.setAuthor(name, 'https://i.imgur.com/wSTFkRM.png', 'https://discord.js.org')
	.setDescription(translationJS[botLang]["info"]["description"])
	.attachFiles(['resources/v-icon.png'])
	.setThumbnail('attachment://resources/v-icon.png')
	.setTimestamp()
	.setFooter(fullName + ' ('+ name +')');
// Help
const helpLayout = new Discord.MessageEmbed()
	.setColor(color)
	.setTitle(translationJS[botLang]["help"]["title"])
	.setDescription("Essa são as coisinhas que sei fazer! Não esqueça de colocar '!' antes de comando hein!")
	.addFields(
		{ name: translationJS[botLang]["help"]["fields"]["nameLang"], value: translationJS[botLang]["help"]["fields"]["valueLang"] },
		{ name: translationJS[botLang]["help"]["fields"]["nameSite"], value: translationJS[botLang]["help"]["fields"]["valueSite"] },
		{ name: translationJS[botLang]["help"]["fields"]["nameHey"], value: translationJS[botLang]["help"]["fields"]["valueHey"] },
		{ name: translationJS[botLang]["help"]["fields"]["nameInfo"], value: translationJS[botLang]["help"]["fields"]["valueInfo"] },
		{ name: translationJS[botLang]["help"]["fields"]["nameHelp"], value: translationJS[botLang]["help"]["fields"]["valueHelp"] },
		{ name: translationJS[botLang]["help"]["fields"]["nameDictionary"], value: translationJS[botLang]["help"]["fields"]["valueDictionary"] },
		{ name: translationJS[botLang]["help"]["fields"]["nameAddw"], value: translationJS[botLang]["help"]["fields"]["valueAddw"] },
		{ name: translationJS[botLang]["help"]["fields"]["nameSeew"], value: translationJS[botLang]["help"]["fields"]["valueSeew"] },
		{ name: translationJS[botLang]["help"]["fields"]["nameEditw"], value: translationJS[botLang]["help"]["fields"]["valueEditw"] },
		{ name: translationJS[botLang]["help"]["fields"]["nameRemw"], value: translationJS[botLang]["help"]["fields"]["valueRemw"] },
	);

bot.on("ready", function(name){
	bot.user.setActivity(activity);

	console.log("Obrigada! Agora estou viva e atualizadaa turururu");
});

bot.on("message", function(msg){
	let args = msg.content.substring(prefix.length).split(" ");

	if(msg.content.startsWith("!")){
		switch(args[0]){
			case "":
				msg.channel.send("Oii " + msg.author.username + "! Tudo bem?  Eu sou a KWiJisho (KawaiiJisho). Se quiser saber o que eu sei fazer, digite !help.");
				break;
			case "lang":
				try{
					msg.channel.send(translationJS[args[1]]["lang"]);
					botLang = args[1];
				}catch(e){
					msg.channel.send("Poxa... eu ainda não sei falar '" + msg.content.substring(6) + "' ainda, talvez um dia eu aprenda huhu.");
				}

				break;
			case "site":
				msg.channel.send(translationJS[botLang]["site"]);
				break;
			case "hey":
				msg.reply(translationJS[botLang]["hey"][1] + msg.content.substring(5) + translationJS[botLang]["hey"][2]);
				break;
			case "info":
				msg.channel.send(infoLayout);
				break;
			case "help":
				msg.channel.send(helpLayout);
				break;
			// DICTIONARY
			case "dictionary":
				// limitOfPage = page * 10;

				// // Dictionary
				// const dictionaryLayout = new Discord.MessageEmbed()
				// 	.setColor(color)
				// 	.setTitle(translationJS[botLang]["help"]["title"])
				// 	.setDescription("Essa são as coisinhas que sei fazer! Não esqueça de colocar '!' antes de comando hein!")
				// 	.addFields(
				// 		{ name: translationJS[botLang]["help"]["fields"]["nameSite"], value: translationJS[botLang]["help"]["fields"]["valueLang"] },
				// 		{ id: id},
				// 	);
				// // for(i = 0; getWordById < countOfWords; i++){

				// // }

				// msg.channel.send(dictionaryLayout);
				msg.channel.send("Tá bem difícil fazer essa função viu " + msg.author.username + " :/, mas ja ja sai...");
				break;
			case "addw":
				try{
					// Removing !addw, title and spaces, just letting the description;
					substringFiltering = 6 + args[1].length + 1;

					countOfWords++;
					countOfWordsUpdate();

					dictionaryFile[args[1]] = {
						id: countOfWords,
						word: args[1],
						desc: msg.content.substring(substringFiltering)
					}

					fs.writeFile(dictionaryFilePath, JSON.stringify(dictionaryFile, null, 4), function(err){
						if(err){
							console.error(err);
							msg.reply("Ops... Não consegui enviar a mensagem, tenta de novo depois, oukai? ;)");
							return;
						}else{
							msg.reply("valeu, tá anotado! 📝 Gostei dessa palavra... '" + args[1] + "'...");
							//Adiciona +1 para contador de palavras
						}
					});
				}catch(e){
					msg.channel.send("Essa palavra já existe! hehe. Digite !seew '" + args[1] + "' para ver ela, você pode editar e remover ela se quiser também! :D");
				}
				break;
			case "seew":
				try{
					const wordLayout = new Discord.MessageEmbed()
						.setColor(color)
						.setTitle(dictionaryFile[args[1]].word.toUpperCase())
						.setDescription(dictionaryFile[args[1]].desc.toLowerCase());
					msg.channel.send(wordLayout);
				}catch(e){
					msg.channel.send("Putz... Desculpa mas não consegui achar essa palavra, que tal criar ela? Digite !addw (palavra) (descrição)");
				}
				break;
			case "editw":
				try{
					// Removing !editw, previous word, new word and spaces
					substringFiltering = 7 + args[1].length + 1 + args[2].length + 1;

					// Deleting old word
					delete dictionaryFile[args[1]];

					dictionaryFile[args[2]] = {
						word: args[2],
						desc: msg.content.substring(substringFiltering)
					}

					fs.writeFile(dictionaryFilePath, JSON.stringify(dictionaryFile, null, 4), function(err){
						if(err){
							console.error(err);
							msg.reply("Ops... Não consegui enviar a mensagem, tenta de novo depois, oukai? ;)");
							return;
						}else{
							msg.channel.send("A palavra '" + args[1] + "' foi mudada para '" + args[2] + "' com sucesso!");
						}
					});
				}catch(e){
					msg.channel.send("Eh... Então, não achei essa palavra que você quer editar, que tal criar ela? Digite !addw (palavra) (descrição)");					
					console.log(e);
				}
				break;
			case "remw":
				try{
					delete dictionaryFile[args[1]];

					fs.writeFile(dictionaryFilePath, JSON.stringify(dictionaryFile, null, 4), function(err){
						if(err){
							console.error(err);
							msg.reply("Ops... Não consegui enviar a mensagem, tenta de novo depois, oukai? ;)");
							return;
						}else{
							msg.channel.send("A palavra '" + args[1] + "' foi apagada com sucesso!");
							//Adiciona +1 para contador de palavras
							countOfWords--;
							countOfWordsUpdate();
						}
					});
				}catch(e){
					msg.channel.send("Eh... Então, não achei essa palavra que você quer apagar...");
					console.log(e);
				}
				break;
			default:
				msg.channel.send(translationJS[botLang]["default"]);
				break;
		}
	}
})

bot.login(token);