using System;
using System.Linq;

namespace KeyGeneralPurposeLibrary.Assets {
  public class KeyGenLibCustomPlotLibrary : KLibAssetLibrary<PlotAsset> {
    public KeyGenLibCustomPlotLibrary() {
      AddAsset(_genericWarPlotAsset, out _genericWarPlotIndex);
      AddAsset(_worldWarPlotAsset, out _worldWarPlotIndex);
      AddAsset(_rebellionPlotAsset, out _rebellionPlotIndex);
      AddAsset(_allianceCreationPlotAsset, out _allianceCreationPlotIndex);
      AddAsset(_allianceJoinPlotAsset, out _allianceJoinPlotIndex);
      AddAsset(_allianceDisbandPlotAsset, out _allianceDisbandPlotIndex);
      AddAsset(_peacePlotAsset, out _peacePlotIndex);
      AddAsset(_assassinationPlotAsset, out _assassinationPlotIndex);
    }
    
    public static int GenericWarPlotIndex => _genericWarPlotIndex;
    public static int WorldWarPlotIndex => _worldWarPlotIndex;
    public static int RebellionPlotIndex => _rebellionPlotIndex;
    public static int AllianceCreationPlotIndex => _allianceCreationPlotIndex;
    public static int AllianceJoinPlotIndex => _allianceJoinPlotIndex;
    public static int AllianceDisbandPlotIndex => _allianceDisbandPlotIndex;
    public static int PeacePlotIndex => _peacePlotIndex;
    public static int AssassinationPlotIndex => _assassinationPlotIndex;
    private static int _genericWarPlotIndex;
    private static int _worldWarPlotIndex;
    private static int _rebellionPlotIndex;
    private static int _allianceCreationPlotIndex;
    private static int _allianceJoinPlotIndex;
    private static int _allianceDisbandPlotIndex;
    private static int _peacePlotIndex;
    private static int _assassinationPlotIndex;

    private readonly PlotAsset _genericWarPlotAsset = new PlotAsset {
      id = "new_war",
      check_initiator_actor = false,
      check_initiator_city = false,
      check_initiator_kingdom = false,
      check_target_actor = false,
      check_target_city = false,
      check_target_kingdom = false,
      check_target_alliance = false,
      check_target_war = false,
      check_launch = (_, __) => true,
      check_should_continue = _ => true,
      check_supporters = PlotsLibrary.checkMembersToRemoveDefault,
      action = plot => World.world.diplomacy.startWar(plot.initiator_kingdom, plot.target_kingdom, WarTypeLibrary.normal) != null,
      path_icon = "plots/icons/plot_new_war",
      time_needed_months = 24,
      cost = 100
    };

    private readonly PlotAsset _worldWarPlotAsset = new PlotAsset {
      id = "total_war",
      check_initiator_actor = false,
      check_initiator_city = false,
      check_initiator_kingdom = false,
      check_target_actor = false,
      check_target_city = false,
      check_target_kingdom = false,
      check_target_alliance = false,
      check_target_war = false,
      check_launch = (_, __) => true,
      check_should_continue = _ => true,
      check_supporters = PlotsLibrary.checkMembersToRemoveDefault,
      action = plot => World.world.diplomacy.startTotalWar(plot.initiator_kingdom, KeyLib.Get<KeyGenLibCustomWarTypeLibrary>()[KeyGenLibCustomWarTypeLibrary.WorldWarWarTypeIndex]) != null,
      path_icon = "plots/icons/plot_new_war",
      time_needed_months = 6,
      cost = 150
    };

    private readonly PlotAsset _rebellionPlotAsset = new PlotAsset {
      id = "rebellion",
      path_icon = "plots/icons/plot_rebellion",
      translation_key = "plot_rebellion",
      description = "plot_description_rebellion",
      check_initiator_city = false,
      check_initiator_kingdom = false,
      check_initiator_actor = false,
      check_supporters = PlotsLibrary.checkMembersToRemoveDefault,
      check_launch = (_, __) => true,
      check_other_plots = (_, __) => false,
      check_should_continue = _ => true,
      plot_power = actor => (int)((actor.stats[S.warfare] + actor.stats[S.intelligence]) * 0.5f),
      action = plot => {
        DiplomacyHelpers.startRebellion(plot.initiator_city, plot.getLeader(), plot);
        return true;
      },
      cost = 500,
      time_needed_months = 6
    };

    private readonly PlotAsset _allianceCreationPlotAsset = new PlotAsset {
      id = "alliance_create",
      path_icon = "plots/icons/plot_alliance_create",
      translation_key = "plot_alliance",
      description = "plot_description_alliance_create",
      check_initiator_city = false,
      check_initiator_kingdom = false,
      check_initiator_actor = false,
      check_supporters = PlotsLibrary.checkMembersToRemoveDefault,
      check_launch = (_, __) => true,
      check_other_plots = (_, __) => false,
      check_should_continue = _ => true,
      plot_power = actor => (int)actor.stats[S.diplomacy],
      action = plot => World.world.alliances.newAlliance(plot.initiator_kingdom, plot.target_kingdom) != null,
      cost = 1000,
      time_needed_months = 6
    };

    private readonly PlotAsset _allianceJoinPlotAsset = new PlotAsset {
      id = "alliance_join",
      path_icon = "plots/icons/plot_alliance_create",
      translation_key = "plot_joining_alliance",
      description = "plot_description_alliance_join",
      check_initiator_city = false,
      check_initiator_kingdom = false,
      check_initiator_actor = false,
      check_supporters = PlotsLibrary.checkMembersToRemoveDefault,
      check_launch = (_, __) => true,
      check_other_plots = (_, __) => false,
      check_should_continue = _ => true,
      plot_power = actor => (int)actor.stats[S.diplomacy],
      action = plot => plot.target_alliance.join(plot.initiator_kingdom),
      cost = 500,
      time_needed_months = 6
    };

    private readonly PlotAsset _allianceDisbandPlotAsset = new PlotAsset {
      id = "alliance_destroy",
      path_icon = "plots/icons/plot_alliance_destroy",
      translation_key = "plot_alliance_dissolution",
      description = "plot_description_alliance_destroy",
      check_initiator_city = false,
      check_initiator_kingdom = false,
      check_initiator_actor = false,
      check_supporters = PlotsLibrary.checkMembersToRemoveDefault,
      check_launch = (_, __) => true,
      check_other_plots = (_, __) => false,
      check_should_continue = _ => true,
      plot_power = actor => (int)actor.stats[S.diplomacy],
      action = plot => {
        Alliance alliance = plot.initiator_kingdom.getAlliance();
        if (alliance != null) World.world.alliances.dissolveAlliance(alliance);
        return true;
      },
      cost = 1500,
      time_needed_months = 6
    };

    private readonly PlotAsset _peacePlotAsset = new PlotAsset {
      id = "stop_war",
      path_icon = "plots/icons/plot_stop_war",
      translation_key = "plot_stop_war",
      description = "plot_description_stop_war",
      check_initiator_city = false,
      check_initiator_kingdom = false,
      check_initiator_actor = false,
      check_supporters = PlotsLibrary.checkMembersToRemoveDefault,
      check_launch = (_, __) => true,
      check_other_plots = (_, __) => false,
      check_should_continue = _ => true,
      plot_power = actor => (int)actor.stats[S.diplomacy],
      action = plot => {
        World.world.wars.endWar(plot.target_war);
        return true;
      },
      cost = 1500,
      time_needed_months = 6
    };

    private readonly PlotAsset _assassinationPlotAsset = new PlotAsset {
      id = "assassination",
      path_icon = "plots/icons/plot_kill_king",
      translation_key = "plot_assassination",
      description = "plot_description_assassination",
      check_initiator_actor = false,
      check_target_actor = false,
      check_supporters = PlotsLibrary.checkMembersToRemoveDefault,
      check_launch = (_, __) => true,
      check_other_plots = (_, __) => false,
      check_should_continue = _ => true,
      plot_power = actor => (int)(actor.stats[S.intelligence] + actor.stats[S.damage]),
      action = plot => {
        Actor strongestSupporter = plot.supporters.ToList()[0];
        for (int i = 0; i < plot.getSupporters(); ++i) {
          if (plot.supporters.ToList()[i].stats[S.damage] > strongestSupporter.stats[S.damage]) {
            strongestSupporter = plot.supporters.ToList()[i];
          }
        }

        if (strongestSupporter.stats[S.damage] > plot.target_actor.stats[S.damage]) {
          int probability = 50;
          if (strongestSupporter.hasTrait("voices_in_my_head")) {
            probability -= 20;
          }

          if (strongestSupporter.hasTrait("death_mark")) {
            probability += 80;
          }

          if (strongestSupporter.hasTrait("veteran")) {
            probability += 10;
          }

          if (strongestSupporter.hasTrait("wise")) {
            probability += 10;
          }

          if (strongestSupporter.hasTrait("strong_minded")) {
            if (strongestSupporter.stats[S.intelligence] < plot.target_actor.stats[S.intelligence] / 2) {
              probability -= 40;
            } else {
              probability += 20;
            }
          }

          if (strongestSupporter.hasTrait("peaceful")) {
            probability -= 40;
          }

          if (strongestSupporter.hasTrait("plague")) {
            probability -= 50;
          }

          if (strongestSupporter.hasTrait("cursed")) {
            probability -= 50;
          }

          if (strongestSupporter.hasTrait("evil")) {
            probability += 20;
          }

          if (strongestSupporter.hasTrait("kingslayer")) {
            probability += 70;
          }

          if (strongestSupporter.hasTrait("tiny")) {
            probability += 5;
          }

          if (strongestSupporter.hasTrait("immortal")) {
            if (strongestSupporter.getAge() < 1000) {
              probability -= 60;
            } else {
              probability += 20;
            }
          }

          if (strongestSupporter.hasTrait("crippled")) {
            probability -= 15;
          }

          if (strongestSupporter.hasTrait("eyepatch")) {
            probability -= 15;
          }

          if (strongestSupporter.hasTrait("skin_burns")) {
            probability -= 15;
          }

          if (strongestSupporter.hasTrait("stupid")) {
            if (strongestSupporter.stats[S.intelligence] < plot.target_actor.stats[S.intelligence] / 2) {
              probability -= 110;
            }
          }

          if (strongestSupporter.hasTrait("genius")) {
            probability += 30;
          }

          if (strongestSupporter.hasTrait("short_sighted")) {
            probability -= 20;
          }

          if (strongestSupporter.hasTrait("unlucky")) {
            probability -= 20;
          }

          if (strongestSupporter.hasTrait("deceitful")) {
            probability += 40;
          }

          if (strongestSupporter.hasTrait("greedy")) {
            probability += 20;
          }

          if (strongestSupporter.hasTrait("honest")) {
            probability -= 20;
          }

          if (strongestSupporter.hasTrait("bloodlust")) {
            probability -= 20;
          }

          if (strongestSupporter.hasTrait("pacifist")) {
            probability -= 80;
          }

          if (strongestSupporter.hasTrait("content")) {
            probability -= 20;
          }

          if (strongestSupporter.hasTrait("paranoid")) {
            if (strongestSupporter.stats[S.intelligence] < plot.target_actor.stats[S.intelligence] / 1.5) {
              probability += 130;
            }
          }

          if (UnityEngine.Random.Range(0, 100) < probability) {
            Kingdom kingdom = plot.target_actor.kingdom;
            plot.target_actor.killHimself();
            strongestSupporter.newKillAction(plot.target_actor, kingdom);
            if (plot._leader != null) {
              kingdom.setKing(plot._leader);
            } else if (plot.initiator_actor != null) {
              kingdom.setKing(plot.initiator_actor);
            } else {
              kingdom.setKing(strongestSupporter);
            }

            return true;
          }
        }

        if (strongestSupporter.stats[S.intelligence] < plot.target_actor.stats[S.intelligence] / 2) {
          Kingdom kingdom = strongestSupporter.kingdom;
          strongestSupporter.killHimself();
          plot.target_actor.newKillAction(strongestSupporter, kingdom);
        }

        return false;
      },
      cost = 1000,
      time_needed_months = 6
    };
  }

  [Obsolete("Use KeyGeneralPurposeLibraryCustomPlotLibrary instead")]
  public class KeyGeneralPurposeLibraryCustomPlotList : KeyGenLibCustomPlotLibrary {
    internal KeyGeneralPurposeLibraryCustomPlotList() { }
  }
}