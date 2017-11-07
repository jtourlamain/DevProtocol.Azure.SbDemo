namespace DevProtocol.Azure.SbDemo.SbReceive

open Microsoft.Azure.ServiceBus

module Sb =
    open System.Threading
    open System.Text
    open System.Threading.Tasks

    let connectionstring = "<your connectionstring>"
    let queuePath = "<your queuename>"

    let receiveData() = 
        let queueClient = QueueClient(connectionstring, queuePath) 
        let exceptionReceivedHandler (args:ExceptionReceivedEventArgs) =
            printfn "Got an exception: %A" args.Exception
            Task.CompletedTask
        let processMessage (message:Message) (token:CancellationToken) =
                printfn "Received message: %s" (Encoding.UTF8.GetString(message.Body))
                queueClient.CompleteAsync(message.SystemProperties.LockToken)|> Async.AwaitTask |> ignore
                Task.CompletedTask
        let msgOptions = new MessageHandlerOptions(fun x -> exceptionReceivedHandler(x))
        msgOptions.AutoComplete <- false
        let messagehandler = queueClient.RegisterMessageHandler(processMessage, msgOptions)
        messagehandler |> ignore