using System.Linq;

namespace KeyGeneralPurposeLibrary.Assets {
  public class KeyGenLibCustomPlotCreator : KLibComponent {
    public void AddPlotToLocalizedLibrary(string id, string description) {
      LocalizedTextManager.instance.localizedText.Remove("plot_" + id);
      LocalizedTextManager.instance.localizedText.Remove("plot_description_" + id + "_info");
      LocalizedTextManager.instance.localizedText.Add("plot_" + id, id);
      LocalizedTextManager.instance.localizedText.Add("plot_description_" + id + "_info", description);
    }

    public Plot CreateAssassinationPlot(PlotAsset plotAsset, Actor initiatorActor, Actor targetActor) {
      Plot plot = World.world.plots.newObject();
      plot.data.plot_type_id = plotAsset.id;
      plot._plot_asset = plotAsset;
      plot.data.name = "Assassination of " + targetActor.coloredName;
      plot.data.founder_name = initiatorActor.coloredName;
      plot.initiator_actor = initiatorActor;
      plot.initiator_city = initiatorActor.city;
      plot.initiator_kingdom = initiatorActor.kingdom;
      plot._leader = initiatorActor;
      plot.addSupporter(initiatorActor);
      if (initiatorActor.getClan() != null) {
        for (int i = 0; i < initiatorActor.getClan().units.ToList().Count; ++i) {
          if (initiatorActor.getClan().units.ToList()[i].Value != initiatorActor) {
            plot.addSupporter(initiatorActor.getClan().units.ToList()[i].Value);
          }
        }
      }

      plot.progress = 0;
      plot.target_actor = targetActor;
      plot.setState(PlotState.Active);
      return plot;
    }

    public Plot CreateWarPlot(PlotAsset plotAsset, Actor initiatorActor, Kingdom targetKingdom) {
      // Plot.newObject() is a method that creates a new Plot object and adds it to the World.world.plots list.
      Plot plot = World.world.plots.newObject();
      plot.data.plot_type_id = plotAsset.id;
      plot._plot_asset = plotAsset;
      plot.data.name = "War from " + initiatorActor.kingdom.name + " against " + targetKingdom.name;
      plot.data.founder_name = initiatorActor.coloredName;
      plot.initiator_actor = initiatorActor;
      plot.initiator_city = initiatorActor.city;
      plot.initiator_kingdom = initiatorActor.kingdom;
      plot._leader = initiatorActor;
      plot.addSupporter(initiatorActor);
      if (initiatorActor.getClan() != null) {
        for (int i = 0; i < initiatorActor.getClan().units.ToList().Count; ++i) {
          if (initiatorActor.getClan().units.ToList()[i].Value != initiatorActor) {
            plot.addSupporter(initiatorActor.getClan().units.ToList()[i].Value);
          }
        }
      }

      plot.progress = 0;
      plot.target_kingdom = targetKingdom;
      plot.setState(PlotState.Active);
      return plot;
    }

    public Plot CreateRebellionPlot(PlotAsset plotAsset, Actor initiatorActor) {
      Plot plot = World.world.plots.newObject();
      plot.data.plot_type_id = plotAsset.id;
      plot._plot_asset = plotAsset;
      plot.data.name = "Rebellion from " + initiatorActor.city.name;
      plot.data.founder_name = initiatorActor.coloredName;
      plot.initiator_actor = initiatorActor;
      plot.initiator_city = initiatorActor.city;
      plot._leader = initiatorActor;
      plot.addSupporter(initiatorActor);
      if (initiatorActor.getClan() != null) {
        for (int i = 0; i < initiatorActor.getClan().units.ToList().Count; ++i) {
          if (initiatorActor.getClan().units.ToList()[i].Value != initiatorActor) {
            if (initiatorActor.getClan().units.ToList()[i].Value.city == initiatorActor.city) {
              plot.addSupporter(initiatorActor.getClan().units.ToList()[i].Value);
            }
          }
        }
      }

      plot.progress = 0;
      plot.setState(PlotState.Active);
      return plot;
    }

    public Plot AllianceCreationPlot(PlotAsset plotAsset, Actor initiatorActor, Kingdom targetKingdom) {
      Plot plot = World.world.plots.newObject();
      plot.data.plot_type_id = plotAsset.id;
      plot._plot_asset = plotAsset;
      plot.data.name = "Alliance between " + initiatorActor.kingdom.name + " and " + targetKingdom.name;
      plot.data.founder_name = initiatorActor.coloredName;
      plot.initiator_actor = initiatorActor;
      plot.initiator_kingdom = initiatorActor.kingdom;
      plot.target_kingdom = targetKingdom;
      plot._leader = initiatorActor;
      plot.addSupporter(initiatorActor);
      if (initiatorActor.getClan() != null) {
        for (int i = 0; i < initiatorActor.getClan().units.ToList().Count; ++i) {
          if (initiatorActor.getClan().units.ToList()[i].Value != initiatorActor) {
            plot.addSupporter(initiatorActor.getClan().units.ToList()[i].Value);
          }
        }
      }

      plot.progress = 0;
      plot.setState(PlotState.Active);
      return plot;
    }

    public Plot JoinAlliancePlot(PlotAsset plotAsset, Actor initiatorActor, Alliance targetAlliance) {
      Plot plot = World.world.plots.newObject();
      plot.data.plot_type_id = plotAsset.id;
      plot._plot_asset = plotAsset;
      plot.data.name = "Get " + initiatorActor.kingdom.name + " into " + targetAlliance.name;
      plot.data.founder_name = initiatorActor.coloredName;
      plot.initiator_actor = initiatorActor;
      plot.initiator_kingdom = initiatorActor.kingdom;
      plot.target_alliance = targetAlliance;
      plot._leader = initiatorActor;
      plot.addSupporter(initiatorActor);
      if (initiatorActor.getClan() != null) {
        for (int i = 0; i < initiatorActor.getClan().units.ToList().Count; ++i) {
          if (initiatorActor.getClan().units.ToList()[i].Value != initiatorActor) {
            plot.addSupporter(initiatorActor.getClan().units.ToList()[i].Value);
          }
        }
      }

      plot.progress = 0;
      plot.setState(PlotState.Active);
      return plot;
    }

    public Plot DisbandAlliancePlot(PlotAsset plotAsset, Actor initiatorActor) {
      Plot plot = World.world.plots.newObject();
      plot.data.plot_type_id = plotAsset.id;
      plot._plot_asset = plotAsset;
      plot.data.name = "Disband " + initiatorActor.kingdom.getAlliance().name;
      plot.data.founder_name = initiatorActor.coloredName;
      plot.initiator_actor = initiatorActor;
      plot.initiator_kingdom = initiatorActor.kingdom;
      plot._leader = initiatorActor;
      plot.addSupporter(initiatorActor);
      if (initiatorActor.getClan() != null) {
        for (int i = 0; i < initiatorActor.getClan().units.ToList().Count; ++i) {
          if (initiatorActor.getClan().units.ToList()[i].Value != initiatorActor) {
            plot.addSupporter(initiatorActor.getClan().units.ToList()[i].Value);
          }
        }
      }

      plot.progress = 0;
      plot.setState(PlotState.Active);
      return plot;
    }

    public Plot PeacePlot(PlotAsset plotAsset, Actor initiatorActor, War targetWar) {
      Plot plot = World.world.plots.newObject();
      plot.data.plot_type_id = plotAsset.id;
      plot._plot_asset = plotAsset;
      plot.data.name = "Stop " + targetWar.name;
      plot.data.founder_name = initiatorActor.coloredName;
      plot.initiator_actor = initiatorActor;
      plot.target_war = targetWar;
      plot._leader = initiatorActor;
      plot.addSupporter(initiatorActor);
      if (initiatorActor.getClan() != null) {
        for (int i = 0; i < initiatorActor.getClan().units.ToList().Count; ++i) {
          if (initiatorActor.getClan().units.ToList()[i].Value != initiatorActor) {
            plot.addSupporter(initiatorActor.getClan().units.ToList()[i].Value);
          }
        }
      }

      plot.progress = 0;
      plot.setState(PlotState.Active);
      return plot;
    }

    public Plot CreateTotalWarPlot(PlotAsset plotAsset, Actor initiatorActor) {
      // Plot.newObject() is a method that creates a new Plot object and adds it to the World.world.plots list.
      Plot plot = World.world.plots.newObject();
      plot.data.plot_type_id = plotAsset.id;
      plot._plot_asset = plotAsset;
      plot.data.name = "War from " + initiatorActor.kingdom.name + " against the whole world";
      plot.data.founder_name = initiatorActor.coloredName;
      plot.initiator_actor = initiatorActor;
      plot.initiator_city = initiatorActor.city;
      plot.initiator_kingdom = initiatorActor.kingdom;
      plot._leader = initiatorActor;
      plot.addSupporter(initiatorActor);
      if (initiatorActor.getClan() != null) {
        for (int i = 0; i < initiatorActor.getClan().units.ToList().Count; ++i) {
          if (initiatorActor.getClan().units.ToList()[i].Value != initiatorActor) {
            plot.addSupporter(initiatorActor.getClan().units.ToList()[i].Value);
          }
        }
      }

      plot.progress = 0;
      plot.setState(PlotState.Active);
      return plot;
    }
  }
}