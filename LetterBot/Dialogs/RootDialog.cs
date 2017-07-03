using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace LetterBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            var message = string.Empty;
            //Generate Greeting

            using (StreamReader r = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"greetings.txt"))
            {
                var greetingList = r.ReadToEnd().Split(new String[] { Environment.NewLine }, StringSplitOptions.None);
                var number = new Random().Next(0, greetingList.Length - 1);

                message += greetingList[number] + "," + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            }

            message += activity.Text + Environment.NewLine + Environment.NewLine;

            using (StreamReader r = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"closings.txt"))
            {
                var closingList = r.ReadToEnd().Split(new String[] { Environment.NewLine }, StringSplitOptions.None);
                var number = new Random().Next(0, closingList.Length - 1);

                message += closingList[number] + "," + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            }

            // return our reply to the user
            await context.PostAsync(message);

            context.Wait(MessageReceivedAsync);
        }
    }
}