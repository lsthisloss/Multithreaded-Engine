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
![ScreenRecording2025-03-20124042-ezgif com-video-to-gif-converter (1)](https://github.com/user-attachments/assets/6782d370-9460-40c0-8a93-9534f114beb5)

##
Pros/Cons: 
+ Simple. Easy to code.
- Hard to scale. Must be full refactored.
