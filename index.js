// COUNTER
var
	i = 0;
// PATHS
const
	dictionaryFilePath = "./storage/dictionaryFile.json",
	translationJSPath = "./storage/translationFile.json",
	metaDataPath = "./storage/metaData.json";
// TRANSLATION
var
	translationJS = require(translationJSPath),
	botLang = "pt",
	supportedLangs = [
		"pt",
		"en",
		"es",
		"ja"
	];
// BOT
const
	Discord = require("discord.js"),
	bot = new Discord.Client();
var
	{ prefix, token, activity } = require("./config.json");
// DICTIONARY
// Storing Data
const
	fs = require("fs");
var
	metaData = require(metaDataPath),
	dictionaryFile = require(dictionaryFilePath),
	substringFiltering = 0,
	deletedWord = 0;
// Page min and max value
var actualPage = 0,
	lastPage = 0,
	// Word min and max value
	firstWordId = 0,
	limitOfPage = 0,
	// Counter but specific for word
	wordCounter = 0,
	// Show word in dictionary
	showWord = [];
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
var name = "Vinícius Gabriel",
	fullName = "Vinícius Gabriel Marques de Melo",
	GitHub = "https://github.com/monambike",
	color = "#8C1EFF";
// Info embed message layout
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
// Help embed message layout
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
				msg.channel.send(translationJS[botLang]["nothing"][1] + msg.author.username + translationJS[botLang]["nothing"][2]);
				break;
			// Command for switch language
			case "lang":
				try{
					botLang = args[1];
					msg.channel.send(translationJS[botLang]["lang"]["try"]);
				}
				catch(e){
					msg.channel.send(translationJS[botLang]["lang"]["catch"][1] + msg.content.substring(6) + translationJS[botLang]["lang"]["catch"][2]);
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
				// Number made by user less 1, to match with code
				actualPage = args[1] - 1;
				// Defining were the words counting will start and end according page number
				firstWordId = actualPage * 10;
				lastWordId = firstWordId + 9;
				// Gets number of pages
				lastPage = 1 + Math.trunc((metaData.countOfWords / 10));
				// A counter but specific for words
				wordCounter = firstWordId;

				// If the value is valid, execute code
				if(args[1] > 0 && args[1] <= lastPage){
					console.log(
						"LOG" + "\n" +
						"+ ------------------- +" + "\n" +
						" - STATS -" + "\n\n" +
						"Initial Word Id: " + firstWordId + "\n" +
						"Last Word Id: " + lastWordId + "\n" +
						"Actual page: " + actualPage + "\n" +
						"Number of pages: " + lastPage + "\n" +
						"Word counter (Initial value): " + wordCounter + "\n" +
						"+ ------------------- +" + "\n" +
						" - FOR -" + "\n\n"
					);

					// Reseting counter and array
					i = 0;
					showWord = [];
					// Inserting values from JSON file to array to better manipulate and avoid errors
					for(wordCounter; wordCounter < metaData.countOfWords; wordCounter++){
						// It won't insert more words in the array than allowed
						if(wordCounter <= lastWordId){
							showWord[i] = dictionaryFile["Words"][wordCounter].word;
							console.log("if: " + " Found in position " + wordCounter + " the word: '" + showWord[i] + "'");
							i++;
						}
						// If it does, breaks the loop
						else{
							console.log("else: Stopped in position: " + wordCounter);
							break;
						}
					}

					try{
						// Dictionary embed message layout
						const dictionaryLayout = new Discord.MessageEmbed()
							.setColor(color)
							.setTitle(translationJS[botLang]["help"]["title"])
							.setDescription(
								translationJS[botLang]["dictionary"]["try"] + "\n\n" +
								 ((firstWordId + 0) + 1) + " - " + showWord[0] + "\n" +
								 ((firstWordId + 1) + 1) + " - " + showWord[1] + "\n" +
								 ((firstWordId + 2) + 1) + " - " + showWord[2] + "\n" +
								 ((firstWordId + 3) + 1) + " - " + showWord[3] + "\n" +
								 ((firstWordId + 4) + 1) + " - " + showWord[4] + "\n" +
								 ((firstWordId + 5) + 1) + " - " + showWord[5] + "\n" +
								 ((firstWordId + 6) + 1) + " - " + showWord[6] + "\n" +
								 ((firstWordId + 7) + 1) + " - " + showWord[7] + "\n" +
								 ((firstWordId + 8) + 1) + " - " + showWord[8] + "\n" +
								 ((firstWordId + 9) + 1) + " - " + showWord[9]
							)
							.setFooter("Page: " + args[1] + " / " + lastPage );

						msg.channel.send(dictionaryLayout);
					}
					catch(e){
						msg.channel.send(translationJS[botLang]["dictionary"]["catch"]);
						console.log("Houve um problema ao carregar o dicionário, e esse aqui é o erro patrão: \n" + e);
					}
				}
				// If the value is invalid
				else{
					msg.channel.send(translationJS[botLang]["dictionary"]["if"][1] + lastPage + translationJS[botLang]["dictionary"]["if"][2]);
					return;
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
							msg.reply(translationJS[botLang]["addw"]["if"]);
							return;
						}
						else{
							msg.reply(translationJS[botLang]["addw"]["else"][1] + args[1] + translationJS[botLang]["addw"]["else"][2]);
							//Adiciona +1 para contador de palavras
						}
					});
				}
				catch(e){
					msg.channel.send(translationJS[botLang]["addw"]["catch"][1] + args[1] + translationJS[botLang]["addw"]["catch"][2]);
				}
				break;
			// Command to see a word from dictionary
			case "seew":
				for(i = 0; i < metaData.countOfWords; i++){
					if(args[1] === dictionaryFile["Words"][i].word){
						// See word embed message layout
						const wordLayout = new Discord.MessageEmbed()
							.setColor(color)
							.setTitle(dictionaryFile["Words"][i].word.toUpperCase())
							.setDescription(dictionaryFile["Words"][i].desc.toLowerCase());
						msg.channel.send(wordLayout);
						return;
					}
					else if(args[1] === dictionaryFile["Words"][i].word && i === metaData.countOfWords){
						msg.channel.send(translationJS[botLang]["seew"]["else"]);
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
								msg.reply(translationJS[botLang]["editw"]["if"]);
								return;
							}
							else{
								msg.channel.send(
									translationJS[botLang]["editw"]["else"][1] +
									args[1] +
									translationJS[botLang]["editw"]["else"][2] +
									args[2] +
									translationJS[botLang]["editw"]["else"][3]
								);
							}
						});
						return;
					}
					else if(args[1] === dictionaryFile["Words"][i].word && i === metaData.countOfWords){
						msg.channel.send(translationJS[botLang]["editw"]["elseif"]);
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
								msg.reply(translationJS[botLang]["remw"]["if"]);
								return;
							}
							else{
								msg.channel.send(translationJS[botLang]["remw"]["else"][1] + args[1] + translationJS[botLang]["remw"]["else"][2]);
								//Adiciona +1 para contador de palavras
								metaData.countOfWords--;
								countOfWordsUpdate();
							}
						});
						return;
					}
					else if(args[1] === dictionaryFile["Words"][i].word && i === metaData.countOfWords){
						msg.channel.send(translationJS[botLang]["remw"]["elseif"]);
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