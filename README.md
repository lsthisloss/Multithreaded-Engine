Simpe C# App that helps do a routine function on multithreaded mod, using system threading and list of threads.   
 
On demo, you can see settings such as threads and timeout number, also a proxies handling.  
The main critical section is showed here, using 'lock' keyword, synchronization primitive that ensures a block of code is executed by only one thread at a time:
```csharp
 lock (locker)
 {
     if (isRunning) // flag var
         return;

     Loader.work = true;
     threadList.Clear(); 

     for (int thrcount = 0; thrcount < numThreads; thrcount++) //num of threads
     {
         Thread th = new Thread(new ThreadStart(Work)); // Create a thread. Every thread do the Work() function
         th.IsBackground = true;
         th.Start();
        threadList.Add(th); // start add thread to the list
     }

     isRunning = true;
     Console.WriteLine("\nWork is started. Threads: " + numThreads);
     Console.ReadKey();
 }
````

![ScreenRecording2025-03-20122317-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/95fa198b-cabf-4cf1-a2b9-fc8064546983)
