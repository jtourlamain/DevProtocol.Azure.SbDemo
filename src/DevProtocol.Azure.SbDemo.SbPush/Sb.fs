namespace DevProtocol.Azure.SbDemo.SbPush

open System.Text
open Microsoft.Azure.ServiceBus

module Sb =
    let connectionstring = "<your connectionstring>"
    let queuePath = "<your queuename>"

    let sendData data =
        let queueClient = QueueClient(connectionstring, queuePath)    
        let sendMessage (queueClient:IQueueClient) (messageText:string) =
            async {
                printfn "%s" messageText
                let message = Message(Encoding.UTF8.GetBytes messageText)
                let! result = queueClient.SendAsync(message) |> Async.AwaitTask
                return result
            }            
        async {
            let result =  List.iter(fun x -> sendMessage queueClient x |> Async.RunSynchronously) data
            queueClient.CloseAsync() |> Async.AwaitTask
            |> ignore
            return result
        }

    let sendTestMessage (messageText:string) =
        let queueClient = QueueClient(connectionstring, queuePath)    
        async {
            printfn "%s" messageText
            let message = Message(Encoding.UTF8.GetBytes messageText)
            let! result = queueClient.SendAsync(message) |> Async.AwaitTask
            return result
        }
       