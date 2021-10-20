using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;
using TwitterUCU;


namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        { 
        PictureProvider provider =  new PictureProvider();
        IPicture picture = provider.GetPicture(@"luke.jpg");
    
        IFilter grey = new FilterGreyscale();
        IFilter negative = new FilterNegative();
        PipeNull pipeNull = new PipeNull();
        PipeSerial pipe2 = new PipeSerial(negative, pipeNull);
        PipeSerial pipe1 = new PipeSerial(grey, pipe2);

        IPicture picture2 = pipe2.Send(picture);
        IPicture picture3 = pipe1.Send(picture2);

        provider.SavePicture(picture2,"..\\..\\luke1.jpg");
        provider.SavePicture(picture3,"..\\..\\luke2.jpg");

        var twitter = new TwitterImage();
        Console.WriteLine(twitter.PublishToTwitter("hola2", @"..\\..\\luke1.jpg"));
        }
    }
}
