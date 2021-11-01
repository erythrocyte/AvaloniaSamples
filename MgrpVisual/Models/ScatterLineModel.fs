module ScatterLineModel

type ScatterLine(x: list<double>, y: list<double>, title: string) =
    member this.x = x
    member this.y = y
    member this.title = title

