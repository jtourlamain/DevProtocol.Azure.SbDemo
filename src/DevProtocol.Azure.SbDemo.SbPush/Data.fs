namespace DevProtocol.Azure.SbDemo.SbPush

module Data =
    let getData() =
        let myData = [1..10]
        myData |>
        List.map (fun x -> x + 10)