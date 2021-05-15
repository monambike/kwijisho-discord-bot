// >>>>>>>>>>   START -  VARIABLES      <<<<<<<<<<
// Desc:
// Region containing some project variables.
// #region - variables

// BOT
const
	// Calling 'discord.js' client
	Discord = require("discord.js"),
	bot = new Discord.Client();
var
	// Settings from 'config.json'
	{ prefix, token, activity } = require("./config.json");




// METADATA
var
	// DEV INFO
	// Basic Info
	metadataName = "Vinícius Gabriel",
	metadataFullName = "Vinícius Gabriel Marques de Melo",
	metadataNickname = "Monambike",
	metadataDiscord = "@Monambike#1728",
	// Links
	metadataLinkGithub = "https://github.com/monambike",
	// LAYOUT
	metadataLayoutColor = "#9638ff";



// PATHS
const
	dictionaryFilePath = "./storage/dictionaryFile.json",
	translationJSPath = "./storage/translationFile.json",
	metaDataPath = "./storage/metaData.json";



// COUNTERS
var
	// Main counter
	i = 0;



// TRANSLATION
var
	// Getting translation file
	translationJS = require(translationJSPath),

	// Default bot language
	botLang = "pt",

	// Linguagens suportadas pelo bot
	supportedLangs = [
		"pt",
		"en",
		"es",
		"ja"
	];



// DICTIONARY
const
	// File System Module from Node.js
	fs = require("fs");

var
	// Getting metadata settings file
	metaData = require(metaDataPath),
	// Getting dictionary file
	dictionaryFile = require(dictionaryFilePath),
	// Substring actual word
	substringToGetRestantDescription = 0;

var
	// Page min and max value
	actualPage = 0,
	lastPage = 0,
	
	// Word min and max value
	firstWordId = 0,

	// Counter but specific for word
	wordCounter = 0,
	
	// Show word in dictionary
	showWord = [];



//#endregion

// >>>>>>>>>>     START - FUNCTIONS     <<<<<<<<<<
// Desc:
// Region containing project functions.
// #region

// Update Counting of Words
function countOfWordsUpdate(){
	fs.writeFile(metaDataPath, JSON.stringify(metaData, null, 4), function(err){
		if(err){
			console.error(err);
			msg.reply("Ops... Houve um problema na contagem...");
			return;
		}
	});
}

// #endregion

// >>>>>>>>>>   START - LAYOUT          <<<<<<<<<<
// Desc:
// Region containing all items for layout constru-
// ction. 
// #region - LAYOUTS

//#endregion

// >>>>>>>>>>   START -  USER MESSAGE   <<<<<<<<<<
// #region - userMessage

bot.on("message", function(msg){
	// Formats prefix
	let args = msg.content.substring(prefix.length).split(" ");

	// Prefix
	if(msg.content.startsWith("!")){
		// First argument
		switch(args[0]){			
			// CUSTOMIZE BOT
			// # ---------- + ---------- + ---------- #

			// CHANGE BOT LANGUAGE
			// Command to switch bot language
			case "lang":
				// If language is supported by the bot
				if(supportedLangs.includes(args[1])){
					// Pass typed language to the variable
					botLang = args[1];

					// Sucess message
					msg.channel.send(translationJS[botLang]["lang"]["try"]);
				}
				// If language is not supported by the bot
				else{
					// Error message
					msg.channel.send(
						translationJS[botLang]["lang"]["catch"][1] +
						msg.content.substring(6) +
						translationJS[botLang]["lang"]["catch"][2]
					);
				}
				break;

			// # ---------- + ---------- + ---------- #
			


			// COMANDOS GERAIS
			// Desc: simple commands like jokes, interactions and others
			// # ---------- + ---------- + ---------- #

			// CONVERSAS COM O BOT
			// Funny talking with bot
			case "hey":
				msg.reply(
					translationJS[botLang]["hey"][1] +
					msg.content.substring(5) + // Said message by user
					translationJS[botLang]["hey"][2]
				);
				break;

			// # ---------- + ---------- + ---------- #
			


			// INFO PAGES
			// # ---------- + ---------- + ---------- #

			// MEU SITE NO GITHUB
			// Command to see my site
			case "github":
				// Show's my Github link
				msg.channel.send(translationJS[botLang]["github"] + metadataLinkGithub);
				break;
			
			// INFO PAGE
			// Command to see more info about me
			case "info":
				// Display info layout
				msg.channel.send(
					new Discord.MessageEmbed()
						.setColor(metadataLayoutColor)

						
						.setURL(metadataLinkGithub)
						.setTitle(translationJS[botLang]["info"]["title"])
						
						.setDescription(translationJS[botLang]["info"]["description"])
						.setThumbnail("https://avatars.githubusercontent.com/u/35270174?v=4")
						.addField("\u200b", "\u200b")

						.setFooter("Discord: " + metadataDiscord + "", msg.author.displayAvatarURL({format: "png"}))
				);
				break;

			// HELP PAGE
			// Help layout
			case "help":
				// Display help layout
				msg.channel.send(
					new Discord.MessageEmbed()
						.setColor(metadataLayoutColor)
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
						)
				);
				break;

			// # ---------- + ---------- + ---------- #
			


			// DICTIONARY
			// Desc: commands related to dictioary such as
			// add words, delete, view, edit and such
			// # ---------- + ---------- + ---------- #

			// MOSTRA O DICIONÁRIO
			// Comando para visualizar o dicionário
			case "dictionary":
				// Number made by user less 1, to match with current code
				actualPage = args[1] - 1;

				// - Defining were the words counting will start and end according page number
				// 10 Words per page
				firstWordId = actualPage * 10;
				// Get the last word
				lastWordId = firstWordId + 9;

				// Gets number of pages
				lastPage = 1 + Math.trunc((metaData.countOfWords / 10));
				
				// A counter specific for words
				wordCounter = firstWordId;

				// Exception case dictionary is empty
				if (isNaN(lastPage)){
					msg.channel.send(
						"Ow " +
						msg.author.username +
						", o dicionário tá vazio cara. Tenta adicionar uma palavra antes com '!addw'."
					)
					break;
				}

				// If the value is valid, execute code
				if(args[1] > 0 && args[1] <= lastPage){
					// Reseting counter and array
					i = 0;
					showWord = [];
					// Inserting values from JSON file to array to better manipulate and avoid errors
					for(wordCounter; wordCounter < metaData.countOfWords; wordCounter++){
						// It won't insert more words in the array than allowed
						if(wordCounter <= lastWordId){
							showWord[i] = dictionaryFile["Words"][wordCounter].word;

							// Add one more to the counter
							i++;
						}
						// If it does, breaks the loop
						else{
							break;
						}
					}

					try{
						// Display dictionary layout
						msg.channel.send(
							new Discord.MessageEmbed()
							.setColor(metadataLayoutColor)
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
								.setFooter("Page: " + args[1] + " / " + lastPage )
						);
					}
					catch(e){
						// If there's an error at loading the dictionary
						msg.channel.send(translationJS[botLang]["dictionary"]["catch"]);
					}
				}
				// If the value is invalid
				else{
					msg.channel.send(
						translationJS[botLang]["dictionary"]["if"][1] +
						lastPage +
						translationJS[botLang]["dictionary"]["if"][2]
					);

					return;
				}
				break;

			// ADD WORD FOR DICTIONARY
			// Command to add a word in the dictionary
			case "addw":
				try{
					wordName = args[1]
					// Removing the comand and all other things that doesnt make part
					// of the description
					wordDescription = msg.content.substring(6 + args[1].length + 1);

					// In the last position add a word
					dictionaryFile["Words"][metaData.countOfWords] = {
						word: wordName,
						desc: wordDescription
					}

					metaData.countOfWords++;

					countOfWordsUpdate();

					fs.writeFile(dictionaryFilePath, JSON.stringify(dictionaryFile, null, 4), function(err){
						// If word already exists in dictionary
						if(err){
							console.error(err);

							msg.reply(translationJS[botLang]["addw"]["if"]);

							return;
						}
						// If the word doesn't exist in the dictionary
						else{
							msg.reply(
								translationJS[botLang]["addw"]["else"][1] +
								args[1] +
								translationJS[botLang]["addw"]["else"][2]
							);
						}
					});
				}
				catch(e){
					msg.channel.send(
						translationJS[botLang]["addw"]["catch"][1] +
						args[1] +
						translationJS[botLang]["addw"]["catch"][2]
					);

					console.log(e.toString());
				}
				break;

			// SEE SPECIFIC WORD FROM DICTIONARY
			// Command to see a word from dictionary
			case "seew":
				for(i = 0; i < metaData.countOfWords; i++){
					if(args[1] === dictionaryFile["Words"][i].word){
						// Display word layout
						msg.channel.send(
							new Discord.MessageEmbed()
							.setColor(metadataLayoutColor)
							.setTitle(dictionaryFile["Words"][i].word.toUpperCase())
								.setDescription(dictionaryFile["Words"][i].desc.toLowerCase())
						);

						return;
					}
					else if(args[1] === dictionaryFile["Words"][i].word && i === metaData.countOfWords){
						msg.channel.send(translationJS[botLang]["seew"]["else"]);
					}
				}
				break;

			// EDIT WORD FROM DICTIONARY
			// Desc: command to edit a word from dictionary
			case "editw":
				for(i = 0; i < metaData.countOfWords; i++){
					if(args[1] === dictionaryFile["Words"][i].word){
						// Removing !editw, previous word, new word and spaces
						substringToGetRestantDescription = (
								7 +
								args[1].length +
								1 +
								args[2].length +
								1
							);

						// Deleting old word
						delete dictionaryFile["Words"][i];

						dictionaryFile["Words"][i] = {
							word: args[2],
							desc: msg.content.substring(substringToGetRestantDescription)
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

			// REMOVE WORD FROM DICTIONARY
			// Desc: command for remove a word from dictionary
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
								msg.channel.send(
									translationJS[botLang]["remw"]["else"][1] +
									args[1] +
									translationJS[botLang]["remw"]["else"][2]
								);
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

			// # ---------- + ---------- + ---------- #

			// EXCEPTIONS
			// # ---------- + ---------- + ---------- #

			// EMPTY MESSAGE
			// Desc: if user doesn't insert message
			case "":
				msg.channel.send(
					translationJS[botLang]["nothing"][1] +
					msg.author.username +
					translationJS[botLang]["nothing"][2]
				);
				break;
			// INEXISTENT COMMAND
			// Desc: if command doesn't exist
			default:
				msg.channel.send(translationJS[botLang]["default"]);
				break;
				
			// # ---------- + ---------- + ---------- #
		}
	}
})

//#endregion

// >>>>>>>>>>   START - STARTING BOT    <<<<<<<<<<
// #region - startingBot

bot.on("ready", function(){
	// Set bot's activity
	bot.user.setActivity(activity);

	// Start bot
	console.log("Obrigada! Agora estou viva e atualizadaa turururu");
});
bot.login(token);

//#endregion