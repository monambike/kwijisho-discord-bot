var botLang = "0";

const Discord = require("discord.js");
const bot = new Discord.Client();

const token = "NzM3NTM1ODQ4MTAyMzYzMjU5.Xx-xyA.ALivCZ6TyjvekWlZ5tSoLzlFW2o";

bot.on("ready", function(name){
	console.log("Obrigada! Agora estou viva e atualizadaa turururu");
});

bot.on("message", function(msg){
	var PREFIX = "!";

	var args = msg.content.substring(PREFIX.lenght).split(" ");

	switch(args[0]){
		case "lang":
			switch(args[1]){
				case "pt":
					msg.channel.sendMessage("Eu agora falo português!");
					break;
				case "en":
					msg.channel.sendMessage("Eu agora falo inglês!");
					break;
				case "es":
					msg.channel.sendMessage("Eu agora falo espanhol!");
					break;
				case "jp":
					msg.channel.sendMessage("Eu agora falo japonês!");
					break;
				default:
					msg.channel.sendMessage("Poxa... eu ainda não sei falar " + msg.content + "ainda...");
			}
		break;
		case "site":
			break;
	}
	msg.reply("espera, você disse '" + user.content.substring(10) + "', agora pouco?");
})

bot.login(token);