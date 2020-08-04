// COUNTER
var i = 0;
// PATHS
const dictionaryFilePath = "./storage/dictionaryFile.json";
const translationJSPath = "./storage/translationFile.json";
const metaDataPath = "./storage/metaData.json";
// TRANSLATION
var translationJS = require(translationJSPath);
var botLang = "pt";
// BOT
const Discord = require("discord.js");
const bot = new Discord.Client();
var { prefix, token, activity } = require("./config.json");
// DICTIONARY
// Storing Data
const fs = require("fs");
var metaData = require(metaDataPath);
var dictionaryFile = require(dictionaryFilePath);
var substringFiltering = 0;
var deletedWord = 0;
// Page
var page = 0;
var limitOfPage = 0;
var initialPageValue = 0;
// Count of Words
function countOfWordsUpdate(){
	fs.writeFile(metaDataPath, JSON.stringify(metaData, null, 4), function(err){
		if(err){
			console.error(err);
			msg.reply("Ops... Houve um problema na contagem...");
			return;
		}
	});
}
// LAYOUT
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
	// Formats prefix
	let args = msg.content.substring(prefix.length).split(" ");

	if(msg.content.startsWith("!")){
		switch(args[0]){
			// If user doesn't insert message
			case "":
				msg.channel.send("Oii " + msg.author.username + "! Tudo bem?  Eu sou a KWiJisho (KawaiiJisho). Se quiser saber o que eu sei fazer, digite !help.");
				break;
			// Command for switch language
			case "lang":
				try{
					msg.channel.send(translationJS[args[1]]["lang"]);
					botLang = args[1];
				}catch(e){
					msg.channel.send("Poxa... eu ainda não sei falar '" + msg.content.substring(6) + "' ainda, talvez um dia eu aprenda huhu.");
				}
				break;
			// Command to see my site
			case "site":
				msg.channel.send(translationJS[botLang]["site"]);
				break;
			// Funny command to talk with bot
			case "hey":
				msg.reply(translationJS[botLang]["hey"][1] + msg.content.substring(5) + translationJS[botLang]["hey"][2]);
				break;
			// Command to see further information
			case "info":
				msg.channel.send(infoLayout);
				break;
			// Command for askin for help
			case "help":
				msg.channel.send(helpLayout);
				break;
			// DICTIONARY
			// Command to see the dictionary
			case "dictionary":
					page = 1;
					initialPageValue = page * 10;
					limitPageValue = initialPageValue + 9;
				try{

					// Dictionary
					const dictionaryLayout = new Discord.MessageEmbed()
						.setColor(color)
						.setTitle(translationJS[botLang]["help"]["title"])
						.setDescription(
							"Essa são as coisinhas que sei fazer! Não esqueça de colocar '!' antes de comando hein!\n\n" +
							 ((initialPageValue + 0) + 1) + " - " + dictionaryFile["Words"][initialPageValue + 0].word + "\n" +
							 ((initialPageValue + 1) + 1) + " - " + dictionaryFile["Words"][initialPageValue + 1].word + "\n" +
							 ((initialPageValue + 2) + 1) + " - " + dictionaryFile["Words"][initialPageValue + 2].word + "\n" +
							 ((initialPageValue + 3) + 1) + " - " + dictionaryFile["Words"][initialPageValue + 3].word + "\n" +
							 ((initialPageValue + 4) + 1) + " - " + dictionaryFile["Words"][initialPageValue + 4].word + "\n" +
							 ((initialPageValue + 5) + 1) + " - " + dictionaryFile["Words"][initialPageValue + 5].word + "\n" +
							 ((initialPageValue + 6) + 1) + " - " + dictionaryFile["Words"][initialPageValue + 6].word + "\n" +
							 ((initialPageValue + 7) + 1) + " - " + dictionaryFile["Words"][initialPageValue + 7].word + "\n" +
							 ((initialPageValue + 8) + 1) + " - " + dictionaryFile["Words"][initialPageValue + 8].word + "\n" +
							 ((initialPageValue + 9) + 1) + " - " + dictionaryFile["Words"][initialPageValue + 9].word + "\n"
						);


					msg.react('738985211013890119')
							.then(() => message.react('738985228261130250'))
							.catch(() => console.error('Ih... Aconteceu um erro ao carregar os emojis do dicionário, olha aqui: ' + e));

					msg.channel.send(dictionaryLayout);
				}catch(e){
					msg.channel.send("Eh... Não cosegui carregar o dicionário :/, tenta depois ok?");
					console.log("Houve um problema ao carregar o dicionário, e esse aqui é o erro patrão: \n" + e);
				}
				break;
			// Command for add a word to the dictionary
			case "addw":
				try{
					// Removing !addw, title and spaces, just letting the description;
					substringFiltering = 6 + args[1].length + 1;

					// In the last position add a word
					dictionaryFile["Words"][metaData.countOfWords] = {
						word: args[1],
						desc: msg.content.substring(substringFiltering)
					}

					metaData.countOfWords++;
					countOfWordsUpdate();

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
			// Command to see a word from dictionary
			case "seew":
				for(i = 0; i < metaData.countOfWords; i++){
					if(args[1] === dictionaryFile["Words"][i].word){
						const wordLayout = new Discord.MessageEmbed()
							.setColor(color)
							.setTitle(dictionaryFile["Words"][i].word.toUpperCase())
							.setDescription(dictionaryFile["Words"][i].desc.toLowerCase());
						msg.channel.send(wordLayout);
						return;
					}else if(args[1] === dictionaryFile["Words"][i].word && i === metaData.countOfWords){
						msg.channel.send("Putz... Desculpa mas não consegui achar essa palavra, que tal criar ela? Digite !addw (palavra) (descrição)");
					}
				}
				break;
			// Command to edit a word from dictionary
			case "editw":
				for(i = 0; i < metaData.countOfWords; i++){
					if(args[1] === dictionaryFile["Words"][i].word){
						// Removing !editw, previous word, new word and spaces
						substringFiltering = 7 + args[1].length + 1 + args[2].length + 1;

						// Deleting old word
						delete dictionaryFile["Words"][i];

						dictionaryFile["Words"][i] = {
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
						return;
					}else if(args[1] === dictionaryFile["Words"][i].word && i === metaData.countOfWords){
						msg.channel.send("Eh... Então, não achei essa palavra que você quer editar, que tal criar ela? Digite !addw (palavra) (descrição)");
					}
				}
				break;
			// Command for remove a word from dictionary
			case "remw":
				for(i = 0; i < metaData.countOfWords; i++){
					if(args[1] === dictionaryFile["Words"][i].word){
						// delete dictionaryFile["Words"][i];
						dictionaryFile["Words"].splice(i,1);

						fs.writeFile(dictionaryFilePath, JSON.stringify(dictionaryFile, null, 4), function(err){
							if(err){
								console.error(err);
								msg.reply("Ops... Não consegui enviar a mensagem, tenta de novo depois, oukai? ;)");
								return;
							}else{
								msg.channel.send("A palavra '" + args[1] + "' foi apagada com sucesso!");
								//Adiciona +1 para contador de palavras
								metaData.countOfWords--;
								countOfWordsUpdate();
							}
						});
						return;
					}else if(args[1] === dictionaryFile["Words"][i].word && i === metaData.countOfWords){
						msg.channel.send("Eh... Então, não achei essa palavra que você quer editar, que tal criar ela? Digite !addw (palavra) (descrição)");
					}
				}
				break;
			// If command doesn't exist
			default:
				msg.channel.send(translationJS[botLang]["default"]);
				break;
		}
	}
})

bot.login(token);