

using System;
using System.Threading.Tasks;
using UnityEngine;

public class ImmVisServiceImpl : ImmVisJupyter.ImmVisJupyterBase
{
    private ScatterplotBehaviour scatterplotBehavior;
    private Dispatcher DispatcherCallback;

    public ImmVisServiceImpl(ScatterplotBehaviour scatterplotBehavior, Dispatcher dispatcherCallback)
    {
        this.scatterplotBehavior = scatterplotBehavior;
        this.DispatcherCallback = dispatcherCallback;
    }

    public override Task<Empty> PlotDataset(DatasetToPlot request, Grpc.Core.ServerCallContext context)
    {
        return Task<Empty>.Run(() =>
        {
            DispatcherCallback.Invoke(() => {
                 scatterplotBehavior?.PlotData(request); 
            });

            return new Empty();
        });
    }

}
