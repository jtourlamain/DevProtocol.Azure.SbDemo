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
                let! result = queueClient.SendAsync(message)
                return result
             }
        async {
            let tasks = List.map(fun x -> sendMessage queueClient x) data
            let! result = Async.Parallel tasks
            do! queueClient.CloseAsync()
            return result
        }

    let sendTestMessage (messageText:string) =
        let queueClient = QueueClient(connectionstring, queuePath)
        async {
            printfn "%s" messageText
            let message = Message(Encoding.UTF8.GetBytes messageText)
            let! result = queueClient.SendAsync(message)
            return result
        }
