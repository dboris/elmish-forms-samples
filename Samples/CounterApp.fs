namespace Samples

open Elmish
open Elmish.Forms

type Model = 
  { Count : int
    Step : int }

type Msg = 
    | Increment 
    | Decrement 
    | Reset
    | SetStep of int

type CounterApp () = 
    inherit Xamarin.Forms.Application ()

    let init () = { Count = 0; Step = 1 }

    let update msg model =
        match msg with
        | Increment -> { model with Count = model.Count + model.Step }
        | Decrement -> { model with Count = model.Count - model.Step }
        | Reset -> init ()
        | SetStep n -> { model with Step = n }

    let view _ _ =
        [ "CounterValue" |> Binding.oneWay (fun m -> m.Count)
          "IncrementCommand" |> Binding.cmd (fun _ _ -> Increment)
          "DecrementCommand" |> Binding.cmd (fun _ _ -> Decrement)
          "ResetCommand" |> Binding.cmdIf (fun _ _ -> Reset) (fun _ m -> m <> init ())
          "StepValue" |> Binding.twoWay (fun m -> double m.Step) (fun v m -> v |> int |> SetStep) ]

    let page = Samples.CounterPage ()

    do
        Program.mkSimple init update view
        |> Program.withConsoleTrace
        |> Program.runPage page

        base.MainPage <- page