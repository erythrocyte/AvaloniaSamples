module FileThreeReaderService

open ScatterLineModel
open System.IO


let parseline(line: string): double * list<double> = 
    let values = line.Split('\t')
    let r = double values.[0]
    let p = double values.[3]
    let s = double values.[4]
    r, [p; s]


let read3ddat (input: string, capt: string) : list<ScatterLine> =
    let l = File.ReadAllLines(input)
    let lines = l.[1..l.Length-2]

    let xy = [for line in lines do yield parseline line]
    let x = [for v in xy do yield fst v]
    let p = [for v in xy do yield (snd v).[0]]
    let s = [for v in xy do yield (snd v).[1]]
    let pl = new ScatterLine(x, p, "p_3d " + capt)
    let sl = new ScatterLine(x, s, "s_3d " + capt)
    // [pl; sl]
    [sl]
