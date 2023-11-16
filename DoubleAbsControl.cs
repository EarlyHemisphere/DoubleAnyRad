using System.Collections;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ModCommon.Util;
using DoubleAnyRad;
using UnityEngine;

internal class DoubleAbsControl : MonoBehaviour
{
	private GameObject abs;
	private GameObject abs2;

	private GameObject _spikeMaster;

	private GameObject _spikeTemplate;

	private GameObject _spikeClone;

	private GameObject _spikeClone2;

	private GameObject _spikeClone3;

	private GameObject _spikeClone4;

	private GameObject _spikeClone5;

	private GameObject _beamsweeper;

	private GameObject _beamsweeper2;
	private GameObject _beamsweeper3;

	private GameObject _beamsweeper4;

	private GameObject _knight;

	private HealthManager _hm;

	private PlayMakerFSM _attackChoices;

	private PlayMakerFSM _attackCommands;

	private PlayMakerFSM _control;

	private PlayMakerFSM _phaseControl;

	private PlayMakerFSM _spikeMasterControl;

	private PlayMakerFSM _beamsweepercontrol;

	private PlayMakerFSM _beamsweeper2control;

	private PlayMakerFSM _spellControl;
	private PlayMakerFSM _attackChoices2;

	private PlayMakerFSM _attackCommands2;

	private PlayMakerFSM _control2;

	private PlayMakerFSM _phaseControl2;

	private PlayMakerFSM _beamsweeper3control;

	private PlayMakerFSM _beamsweeper4control;

	private int CWRepeats = 0;
	private int CWRepeats2 = 0;

	private bool fullSpikesSet = false;

	private bool disableBeamSet = false;

	private bool arena2Set = false;

	private bool onePlatSet = false;

	private bool platSpikesSet = false;

	private const int fullSpikesHealth = 750;

	private const int onePlatHealth = 500;

	private const int platSpikesHealth = 500;

	private const float nailWallDelay = 0.8f;
	private int damageDealt = 0;
	private bool flagP2 = false;
	private bool flagP3 = false;
	private bool flagP4 = false;
	private bool flagP5 = false;
	private bool flagDie = false;
	private bool flag2 = false;
	private bool endFlag = false;

	private void Start()
	{
		abs = GameObject.Find("Absolute Radiance");
		abs2 = Instantiate(abs);
		_hm = abs.GetComponent<HealthManager>();
		_attackChoices = abs.LocateMyFSM("Attack Choices");
		_attackCommands = abs.LocateMyFSM("Attack Commands");
		_control = abs.LocateMyFSM("Control");
		_phaseControl = abs.LocateMyFSM("Phase Control");
		_spikeMaster = GameObject.Find("Spike Control");
		_spikeMasterControl = _spikeMaster.LocateMyFSM("Control");
		_spikeTemplate = GameObject.Find("Radiant Spike");
		_beamsweeper = GameObject.Find("Beam Sweeper");
		_beamsweeper2 = UnityEngine.Object.Instantiate(_beamsweeper);
		_beamsweeper2.AddComponent<BeamSweeperClone>();
		_beamsweepercontrol = _beamsweeper.LocateMyFSM("Control");
		_beamsweeper2control = _beamsweeper2.LocateMyFSM("Control");
		_knight = GameObject.Find("Knight");
		_spellControl = _knight.LocateMyFSM("Spell Control");
		_attackChoices2 = abs2.LocateMyFSM("Attack Choices");
		_attackCommands2 = abs2.LocateMyFSM("Attack Commands");
		_control2 = abs2.LocateMyFSM("Control");
		_phaseControl2 = abs2.LocateMyFSM("Phase Control");
		_beamsweeper3 = GameObject.Find("Beam Sweeper");
		_beamsweeper4 = UnityEngine.Object.Instantiate(_beamsweeper);
		_beamsweeper4.AddComponent<BeamSweeperClone>();
		_beamsweeper3control = _beamsweeper3.LocateMyFSM("Control");
		_beamsweeper4control = _beamsweeper4.LocateMyFSM("Control");

		FsmutilExt.GetAction<SetHP>(_control, "Scream", 7).hp = 20000;
		FsmutilExt.GetAction<SetHP>(_control2, "Scream", 7).hp = 20000;
		_spikeClone = UnityEngine.Object.Instantiate(_spikeTemplate);
		_spikeClone.transform.SetPositionX(58f);
		_spikeClone.transform.SetPositionY(153.8f);
		_spikeClone2 = UnityEngine.Object.Instantiate(_spikeTemplate);
		_spikeClone2.transform.SetPositionX(57.5f);
		_spikeClone2.transform.SetPositionY(153.8f);
		_spikeClone3 = UnityEngine.Object.Instantiate(_spikeTemplate);
		_spikeClone3.transform.SetPositionX(57f);
		_spikeClone3.transform.SetPositionY(153.8f);
		_spikeClone4 = UnityEngine.Object.Instantiate(_spikeTemplate);
		_spikeClone4.transform.SetPositionX(58.5f);
		_spikeClone4.transform.SetPositionY(153.8f);
		_spikeClone5 = UnityEngine.Object.Instantiate(_spikeTemplate);
		_spikeClone5.transform.SetPositionX(59f);
		_spikeClone5.transform.SetPositionY(153.8f);
		_spikeClone.LocateMyFSM("Control").SendEvent("DOWN");
		_spikeClone2.LocateMyFSM("Control").SendEvent("DOWN");
		_spikeClone3.LocateMyFSM("Control").SendEvent("DOWN");
		_spikeClone4.LocateMyFSM("Control").SendEvent("DOWN");
		_spikeClone5.LocateMyFSM("Control").SendEvent("DOWN");
		FsmutilExt.GetAction<Wait>(_attackCommands, "Orb Antic", 0).time = 0.1f;
		FsmutilExt.GetAction<SetIntValue>(_attackCommands, "Orb Antic", 1).intValue = 12;
		FsmutilExt.GetAction<RandomInt>(_attackCommands, "Orb Antic", 2).min = 10;
		FsmutilExt.GetAction<RandomInt>(_attackCommands, "Orb Antic", 2).max = 14;
		FsmutilExt.GetAction<Wait>(_attackCommands, "Orb Summon", 2).time = 0.1f;
		FsmutilExt.GetAction<Wait>(_attackCommands, "Orb Pause", 0).time = 0.1f;
		FsmutilExt.GetAction<Wait>(_attackChoices, "Orb Recover", 0).time = 0.5f;
		FsmutilExt.GetAction<Wait>(_attackCommands, "CW Repeat", 0).time = -0.5f;
		FsmutilExt.GetAction<Wait>(_attackCommands, "CCW Repeat", 0).time = -0.5f;
		FsmutilExt.GetAction<FloatAdd>(_attackCommands, "CW Restart", 2).add = -10f;
		FsmutilExt.GetAction<FloatAdd>(_attackCommands, "CCW Restart", 2).add = 10f;
		FsmutilExt.RemoveAction(_attackCommands, "CW Restart", 1);
		FsmutilExt.RemoveAction(_attackCommands, "CCW Restart", 1);
		FsmutilExt.GetAction<Wait>(_attackChoices, "Beam Sweep L", 0).time = 0.5f;
		FsmutilExt.GetAction<Wait>(_attackChoices, "Beam Sweep R", 0).time = 0.5f;
		FsmutilExt.ChangeTransition(_attackChoices, "A1 Choice", "BEAM SWEEP R", "Beam Sweep L");
		FsmutilExt.ChangeTransition(_attackChoices, "A2 Choice", "BEAM SWEEP R", "Beam Sweep L 2");
		FsmutilExt.GetAction<Wait>(_attackChoices, "Beam Sweep L 2", 0).time = 5.05f;
		FsmutilExt.GetAction<Wait>(_attackChoices, "Beam Sweep R 2", 0).time = 5.05f;
		FsmutilExt.GetAction<SendEventByName>(_attackChoices, "Beam Sweep L 2", 1).sendEvent = "BEAM SWEEP L";
		FsmutilExt.GetAction<SendEventByName>(_attackChoices, "Beam Sweep R 2", 1).sendEvent = "BEAM SWEEP R";
		FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 1", 9).delay = 0.3f;
		FsmutilExt.GetAction<Wait>(_attackCommands, "EB 1", 10).time = 0.5f;
		FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 2", 9).delay = 0.3f;
		FsmutilExt.GetAction<Wait>(_attackCommands, "EB 2", 10).time = 0.5f;
		FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 3", 9).delay = 0.3f;
		FsmutilExt.GetAction<Wait>(_attackCommands, "EB 3", 10).time = 0.5f;
		FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 4", 4).delay = 0.3f;
		FsmutilExt.GetAction<Wait>(_attackCommands, "EB 4", 5).time = 0.5f;
		FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 5", 5).delay = 0.3f;
		FsmutilExt.GetAction<Wait>(_attackCommands, "EB 5", 6).time = 0.5f;
		FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 6", 5).delay = 0.3f;
		FsmutilExt.GetAction<Wait>(_attackCommands, "EB 6", 6).time = 0.5f;
		FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 7", 8).delay = 0.3f;
		FsmutilExt.GetAction<Wait>(_attackCommands, "EB 7", 9).time = 0.5f;
		FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 8", 8).delay = 0.3f;
		FsmutilExt.GetAction<Wait>(_attackCommands, "EB 8", 9).time = 0.5f;
		FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 9", 8).delay = 0.3f;
		FsmutilExt.GetAction<Wait>(_attackCommands, "EB 9", 9).time = 0.5f;
		FsmutilExt.GetAction<SendEventByName>(_attackCommands, "Aim", 10).delay = 1f;
		FsmutilExt.GetAction<Wait>(_attackCommands, "Aim", 11).time = 0.5f;
		FsmutilExt.GetAction<Wait>(_attackCommands, "Eb Extra Wait", 0).time = 0.05f;
		FsmutilExt.GetAction<SendEventByName>(_attackChoices, "Nail Top Sweep", 1).delay = 0.35f;
		FsmutilExt.GetAction<SendEventByName>(_attackChoices, "Nail Top Sweep", 2).delay = 0.7f;
		FsmutilExt.GetAction<SendEventByName>(_attackChoices, "Nail Top Sweep", 3).delay = 1.05f;
		FsmutilExt.GetAction<Wait>(_attackChoices, "Nail Top Sweep", 4).time = 2.3f;
		FsmutilExt.GetAction<Wait>(_control, "Rage Comb", 0).time = 0.35f;
		FsmutilExt.GetAction<SendEventByName>(_attackChoices, "Nail L Sweep", 1).delay = 0.25f;
		FsmutilExt.GetAction<SendEventByName>(_attackChoices, "Nail L Sweep", 1).delay = 1.85f;
		FsmutilExt.GetAction<SendEventByName>(_attackChoices, "Nail L Sweep", 2).delay = 3.45f;
		FsmutilExt.GetAction<Wait>(_attackChoices, "Nail L Sweep", 3).time = 5f;
		FsmutilExt.GetAction<SendEventByName>(_attackChoices, "Nail R Sweep", 1).delay = 0.25f;
		FsmutilExt.GetAction<SendEventByName>(_attackChoices, "Nail R Sweep", 1).delay = 1.85f;
		FsmutilExt.GetAction<SendEventByName>(_attackChoices, "Nail R Sweep", 2).delay = 3.45f;
		FsmutilExt.GetAction<Wait>(_attackChoices, "Nail R Sweep", 3).time = 5f;
		FsmutilExt.GetAction<Wait>(_attackCommands2, "Orb Antic", 0).time = 0.1f;
		FsmutilExt.GetAction<SetIntValue>(_attackCommands2, "Orb Antic", 1).intValue = 12;
		FsmutilExt.GetAction<RandomInt>(_attackCommands2, "Orb Antic", 2).min = 10;
		FsmutilExt.GetAction<RandomInt>(_attackCommands2, "Orb Antic", 2).max = 14;
		FsmutilExt.GetAction<Wait>(_attackCommands2, "Orb Summon", 2).time = 0.1f;
		FsmutilExt.GetAction<Wait>(_attackCommands2, "Orb Pause", 0).time = 0.1f;
		FsmutilExt.GetAction<Wait>(_attackChoices2, "Orb Recover", 0).time = 0.5f;
		FsmutilExt.GetAction<Wait>(_attackCommands2, "CW Repeat", 0).time = -0.5f;
		FsmutilExt.GetAction<Wait>(_attackCommands2, "CCW Repeat", 0).time = -0.5f;
		FsmutilExt.GetAction<FloatAdd>(_attackCommands2, "CW Restart", 2).add = -10f;
		FsmutilExt.GetAction<FloatAdd>(_attackCommands2, "CCW Restart", 2).add = 10f;
		FsmutilExt.RemoveAction(_attackCommands2, "CW Restart", 1);
		FsmutilExt.RemoveAction(_attackCommands2, "CCW Restart", 1);
		FsmutilExt.GetAction<Wait>(_attackChoices2, "Beam Sweep L", 0).time = 0.5f;
		FsmutilExt.GetAction<Wait>(_attackChoices2, "Beam Sweep R", 0).time = 0.5f;
		FsmutilExt.ChangeTransition(_attackChoices2, "A1 Choice", "BEAM SWEEP R", "Beam Sweep L");
		FsmutilExt.ChangeTransition(_attackChoices2, "A2 Choice", "BEAM SWEEP R", "Beam Sweep L 2");
		FsmutilExt.GetAction<Wait>(_attackChoices2, "Beam Sweep L 2", 0).time = 5.05f;
		FsmutilExt.GetAction<Wait>(_attackChoices2, "Beam Sweep R 2", 0).time = 5.05f;
		FsmutilExt.GetAction<SendEventByName>(_attackChoices2, "Beam Sweep L 2", 1).sendEvent = "BEAM SWEEP L";
		FsmutilExt.GetAction<SendEventByName>(_attackChoices2, "Beam Sweep R 2", 1).sendEvent = "BEAM SWEEP R";
		FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 1", 9).delay = 0.3f;
		FsmutilExt.GetAction<Wait>(_attackCommands2, "EB 1", 10).time = 0.5f;
		FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 2", 9).delay = 0.3f;
		FsmutilExt.GetAction<Wait>(_attackCommands2, "EB 2", 10).time = 0.5f;
		FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 3", 9).delay = 0.3f;
		FsmutilExt.GetAction<Wait>(_attackCommands2, "EB 3", 10).time = 0.5f;
		FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 4", 4).delay = 0.3f;
		FsmutilExt.GetAction<Wait>(_attackCommands2, "EB 4", 5).time = 0.5f;
		FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 5", 5).delay = 0.3f;
		FsmutilExt.GetAction<Wait>(_attackCommands2, "EB 5", 6).time = 0.5f;
		FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 6", 5).delay = 0.3f;
		FsmutilExt.GetAction<Wait>(_attackCommands2, "EB 6", 6).time = 0.5f;
		FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 7", 8).delay = 0.3f;
		FsmutilExt.GetAction<Wait>(_attackCommands2, "EB 7", 9).time = 0.5f;
		FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 8", 8).delay = 0.3f;
		FsmutilExt.GetAction<Wait>(_attackCommands2, "EB 8", 9).time = 0.5f;
		FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 9", 8).delay = 0.3f;
		FsmutilExt.GetAction<Wait>(_attackCommands2, "EB 9", 9).time = 0.5f;
		FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "Aim", 10).delay = 1f;
		FsmutilExt.GetAction<Wait>(_attackCommands2, "Aim", 11).time = 0.5f;
		FsmutilExt.GetAction<Wait>(_attackCommands2, "Eb Extra Wait", 0).time = 0.05f;
		FsmutilExt.GetAction<SendEventByName>(_attackChoices2, "Nail Top Sweep", 1).delay = 0.35f;
		FsmutilExt.GetAction<SendEventByName>(_attackChoices2, "Nail Top Sweep", 2).delay = 0.7f;
		FsmutilExt.GetAction<SendEventByName>(_attackChoices2, "Nail Top Sweep", 3).delay = 1.05f;
		FsmutilExt.GetAction<Wait>(_attackChoices2, "Nail Top Sweep", 4).time = 2.3f;
		FsmutilExt.GetAction<Wait>(_control2, "Rage Comb", 0).time = 0.35f;
		FsmutilExt.GetAction<SendEventByName>(_attackChoices2, "Nail L Sweep", 1).delay = 0.25f;
		FsmutilExt.GetAction<SendEventByName>(_attackChoices2, "Nail L Sweep", 1).delay = 1.85f;
		FsmutilExt.GetAction<SendEventByName>(_attackChoices2, "Nail L Sweep", 2).delay = 3.45f;
		FsmutilExt.GetAction<Wait>(_attackChoices2, "Nail L Sweep", 3).time = 5f;
		FsmutilExt.GetAction<SendEventByName>(_attackChoices2, "Nail R Sweep", 1).delay = 0.25f;
		FsmutilExt.GetAction<SendEventByName>(_attackChoices2, "Nail R Sweep", 1).delay = 1.85f;
		FsmutilExt.GetAction<SendEventByName>(_attackChoices2, "Nail R Sweep", 2).delay = 3.45f;
		FsmutilExt.GetAction<Wait>(_attackChoices2, "Nail R Sweep", 3).time = 5f;
		AddNailWall("Nail L Sweep", "COMB R", 1.3f, 1);
		AddNailWall("Nail R Sweep", "COMB L", 1.3f, 1);
		AddNailWall("Nail L Sweep", "COMB R", 2.9f, 1);
		AddNailWall("Nail R Sweep", "COMB L", 2.9f, 1);
		AddNailWall("Nail L Sweep 2", "COMB R2", 1f, 1);
		AddNailWall("Nail R Sweep 2", "COMB L2", 1f, 1);
		AddNailWall2("Nail L Sweep", "COMB R", 1.3f, 1);
		AddNailWall2("Nail R Sweep", "COMB L", 1.3f, 1);
		AddNailWall2("Nail L Sweep", "COMB R", 2.9f, 1);
		AddNailWall2("Nail R Sweep", "COMB L", 2.9f, 1);
		AddNailWall2("Nail L Sweep 2", "COMB R2", 1f, 1);
		AddNailWall2("Nail R Sweep 2", "COMB L2", 1f, 1);
		FsmutilExt.AddAction(_control, "Death Ready", new Trigger2dEvent {
			trigger = 0,
			collideTag = "",
			collideLayer = "Attack",
			sendEvent = new FsmEvent("FINAL HIT"),
			storeCollider = null
		});
		FsmutilExt.AddAction(_control, "Death Ready", new Trigger2dEvent {
			trigger = 0,
			collideTag = "",
			collideLayer = "Orbit Shield",
			sendEvent = new FsmEvent("FINAL HIT"),
			storeCollider = null
		});
		FsmutilExt.AddAction(_control2, "Death Ready", new Trigger2dEvent {
			trigger = 0,
			collideTag = "",
			collideLayer = "Attack",
			sendEvent = new FsmEvent("FINAL HIT"),
			storeCollider = null
		});
		FsmutilExt.AddAction(_control2, "Death Ready", new Trigger2dEvent {
			trigger = 0,
			collideTag = "",
			collideLayer = "Orbit Shield",
			sendEvent = new FsmEvent("FINAL HIT"),
			storeCollider = null
		});

		On.HealthManager.TakeDamage += KnightHit;
		StartCoroutine(Init());

		Log("fin.");
		Log("Do you actually think you can win this fight?");
	}

	private void KnightHit(On.HealthManager.orig_TakeDamage orig, HealthManager self, HitInstance hitInstance) {
		orig(self, hitInstance);
		damageDealt += hitInstance.DamageDealt;
	}

	private IEnumerator Init() {
		yield return new WaitForSeconds(2f);
		
		abs.GetComponent<HealthManager>().hp = 999999;
		abs2.GetComponent<HealthManager>().hp = 999999;

		flagP2 = false;
		flagP3 = false;
		flagP4 = false;
		flagP5 = false;
		flagDie = false;
		flag2 = false;
		endFlag = false;
	}

	private void Update()
	{
		if (_attackCommands.FsmVariables.GetFsmBool("Repeated").Value)
		{
			switch (CWRepeats)
			{
			case 1:
				CWRepeats = 2;
				break;
			case 0:
				CWRepeats = 1;
				_attackCommands.FsmVariables.GetFsmBool("Repeated").Value = false;
				break;
			}
		}
		else if (CWRepeats == 2)
		{
			CWRepeats = 0;
		}
		if (_attackCommands2.FsmVariables.GetFsmBool("Repeated").Value)
		{
			switch (CWRepeats2)
			{
			case 1:
				CWRepeats2 = 2;
				break;
			case 0:
				CWRepeats2 = 1;
				_attackCommands2.FsmVariables.GetFsmBool("Repeated").Value = false;
				break;
			}
		}
		else if (CWRepeats2 == 2)
		{
			CWRepeats2 = 0;
		}
		if (_beamsweepercontrol.ActiveStateName == _beamsweeper2control.ActiveStateName)
		{
			string activeStateName = _beamsweepercontrol.ActiveStateName;
			string text = activeStateName;
			if (text != null)
			{
				if (!(text == "Beam Sweep L"))
				{
					if (text == "Beam Sweep R")
					{
						_beamsweeper2control.ChangeState(GetFsmEventByName(_beamsweeper2control, "BEAM SWEEP L"));
					}
				}
				else
				{
					_beamsweeper2control.ChangeState(GetFsmEventByName(_beamsweeper2control, "BEAM SWEEP R"));
				}
			}
		}
		if (_beamsweeper3control.ActiveStateName == _beamsweeper4control.ActiveStateName)
		{
			string activeStateName = _beamsweeper3control.ActiveStateName;
			string text = activeStateName;
			if (text != null)
			{
				if (!(text == "Beam Sweep L"))
				{
					if (text == "Beam Sweep R")
					{
						_beamsweeper4control.ChangeState(GetFsmEventByName(_beamsweeper4control, "BEAM SWEEP L"));
					}
				}
				else
				{
					_beamsweeper4control.ChangeState(GetFsmEventByName(_beamsweeper4control, "BEAM SWEEP R"));
				}
			}
		}

		if (damageDealt >= 1500 && damageDealt < 3900) {
			if (!flagP2) {
				abs.LocateMyFSM("Phase Control").SetState("Set Phase 2");
				abs2.LocateMyFSM("Phase Control").SetState("Set Phase 2");
				flagP2 = true;
			}
		} else if (damageDealt >= 3900 && damageDealt < 4500) {
			if (!flagP3) {
				abs.LocateMyFSM("Phase Control").SetState("Set Phase 3");
				abs2.LocateMyFSM("Phase Control").SetState("Set Phase 3");
				flagP3 = true;
			}
		} else if (damageDealt >= 4500 && damageDealt < 6000) {
			if (!flagP4) {
				abs.LocateMyFSM("Phase Control").SetState("Stun 1");
				abs2.LocateMyFSM("Phase Control").SetState("Stun 1");
				flagP4 = true;
			}
		} else if (damageDealt >= 6000 && damageDealt < 8560) {
			if (!flagP5) {
				abs.LocateMyFSM("Phase Control").SetState("Set Ascend");
				abs2.LocateMyFSM("Phase Control").SetState("Set Ascend");
				flagP5 = true;
			}
		} else if (damageDealt >= 8560) {
			if (!flagDie) {
				abs.LocateMyFSM("Control").Fsm.Variables.GetFsmInt("Death HP").Value = 9999999;
				abs2.LocateMyFSM("Control").Fsm.Variables.GetFsmInt("Death HP").Value = 9999999;
				flagDie = true;
			}
		}

		if (abs.LocateMyFSM("Control").ActiveStateName == "Arena 2 Start" || abs2.LocateMyFSM("Control").ActiveStateName == "Arena 2 Start") {
			if (!flag2) {
				abs.LocateMyFSM("Control").SetState("Arena 2 Start");
				abs.LocateMyFSM("Attack Choices").SetState("A1 End");
				abs2.LocateMyFSM("Control").SetState("Arena 2 Start");
				abs2.LocateMyFSM("Attack Choices").SetState("A1 End");
				flag2 = true;
			}
		}

		if (abs.LocateMyFSM("Control").ActiveStateName == "Final Impact" || abs2.LocateMyFSM("Control").ActiveStateName == "Final Impact") {
			if (!endFlag) {
				abs.LocateMyFSM("Control").SetState("Final Impact");
				abs2.LocateMyFSM("Control").SetState("Final Impact");
				endFlag = true;
			}
		}

		if (damageDealt >= 3000 && !fullSpikesSet)
		{
			fullSpikesSet = true;
			FsmutilExt.GetAction<SendEventByName>(_spikeMasterControl, "Spikes Left", 0).sendEvent = "UP";
			FsmutilExt.GetAction<SendEventByName>(_spikeMasterControl, "Spikes Left", 1).sendEvent = "UP";
			FsmutilExt.GetAction<SendEventByName>(_spikeMasterControl, "Spikes Left", 2).sendEvent = "UP";
			FsmutilExt.GetAction<SendEventByName>(_spikeMasterControl, "Spikes Left", 3).sendEvent = "UP";
			FsmutilExt.GetAction<SendEventByName>(_spikeMasterControl, "Spikes Left", 4).sendEvent = "UP";
			FsmutilExt.GetAction<SendEventByName>(_spikeMasterControl, "Spikes Right", 0).sendEvent = "UP";
			FsmutilExt.GetAction<SendEventByName>(_spikeMasterControl, "Spikes Right", 1).sendEvent = "UP";
			FsmutilExt.GetAction<SendEventByName>(_spikeMasterControl, "Spikes Right", 2).sendEvent = "UP";
			FsmutilExt.GetAction<SendEventByName>(_spikeMasterControl, "Spikes Right", 3).sendEvent = "UP";
			FsmutilExt.GetAction<SendEventByName>(_spikeMasterControl, "Spikes Right", 4).sendEvent = "UP";
			FsmutilExt.GetAction<SendEventByName>(_spikeMasterControl, "Wave L", 2).sendEvent = "UP";
			FsmutilExt.GetAction<SendEventByName>(_spikeMasterControl, "Wave L", 3).sendEvent = "UP";
			FsmutilExt.GetAction<SendEventByName>(_spikeMasterControl, "Wave L", 4).sendEvent = "UP";
			FsmutilExt.GetAction<SendEventByName>(_spikeMasterControl, "Wave L", 5).sendEvent = "UP";
			FsmutilExt.GetAction<SendEventByName>(_spikeMasterControl, "Wave L", 6).sendEvent = "UP";
			FsmutilExt.GetAction<WaitRandom>(_spikeMasterControl, "Wave L", 7).timeMin = 0.1f;
			FsmutilExt.GetAction<WaitRandom>(_spikeMasterControl, "Wave L", 7).timeMax = 0.1f;
			FsmutilExt.GetAction<SendEventByName>(_spikeMasterControl, "Wave R", 2).sendEvent = "UP";
			FsmutilExt.GetAction<SendEventByName>(_spikeMasterControl, "Wave R", 3).sendEvent = "UP";
			FsmutilExt.GetAction<SendEventByName>(_spikeMasterControl, "Wave R", 4).sendEvent = "UP";
			FsmutilExt.GetAction<SendEventByName>(_spikeMasterControl, "Wave R", 5).sendEvent = "UP";
			FsmutilExt.GetAction<SendEventByName>(_spikeMasterControl, "Wave R", 6).sendEvent = "UP";
			FsmutilExt.GetAction<WaitRandom>(_spikeMasterControl, "Wave R", 7).timeMin = 0.1f;
			FsmutilExt.GetAction<WaitRandom>(_spikeMasterControl, "Wave R", 7).timeMax = 0.1f;
			_spikeMasterControl.SetState("Spike Waves");
			FsmutilExt.GetAction<Wait>(_attackCommands, "Orb Summon", 2).time = 0.1f;
			FsmutilExt.GetAction<SetIntValue>(_attackCommands, "Orb Antic", 1).intValue = 12;
			FsmutilExt.GetAction<RandomInt>(_attackCommands, "Orb Antic", 2).min = 10;
			FsmutilExt.GetAction<RandomInt>(_attackCommands, "Orb Antic", 2).max = 14;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 1", 2).delay = 0.3f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 1", 3).delay = 0.5f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 1", 8).delay = 0.5f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 1", 9).delay = 0.5f;
			FsmutilExt.GetAction<Wait>(_attackCommands, "EB 1", 10).time = 0.5f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 2", 3).delay = 0.3f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 2", 4).delay = 0.5f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 2", 8).delay = 0.5f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 2", 9).delay = 0.5f;
			FsmutilExt.GetAction<Wait>(_attackCommands, "EB 2", 10).time = 0.5f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 3", 3).delay = 0.3f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 3", 4).delay = 0.5f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 3", 8).delay = 0.5f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands, "EB 3", 9).delay = 0.5f;
			FsmutilExt.GetAction<Wait>(_attackCommands, "EB 3", 10).time = 0.5f;
			FsmutilExt.GetAction<Wait>(_attackCommands2, "Orb Summon", 2).time = 0.1f;
			FsmutilExt.GetAction<SetIntValue>(_attackCommands2, "Orb Antic", 1).intValue = 12;
			FsmutilExt.GetAction<RandomInt>(_attackCommands2, "Orb Antic", 2).min = 10;
			FsmutilExt.GetAction<RandomInt>(_attackCommands2, "Orb Antic", 2).max = 14;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 1", 2).delay = 0.3f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 1", 3).delay = 0.5f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 1", 8).delay = 0.5f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 1", 9).delay = 0.5f;
			FsmutilExt.GetAction<Wait>(_attackCommands2, "EB 1", 10).time = 0.5f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 2", 3).delay = 0.3f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 2", 4).delay = 0.5f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 2", 8).delay = 0.5f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 2", 9).delay = 0.5f;
			FsmutilExt.GetAction<Wait>(_attackCommands2, "EB 2", 10).time = 0.5f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 3", 3).delay = 0.3f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 3", 4).delay = 0.5f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 3", 8).delay = 0.5f;
			FsmutilExt.GetAction<SendEventByName>(_attackCommands2, "EB 3", 9).delay = 0.5f;
			FsmutilExt.GetAction<Wait>(_attackCommands2, "EB 3", 10).time = 0.5f;
		}
		if (damageDealt >= 3870 && !disableBeamSet)
		{
			disableBeamSet = true;
			FsmutilExt.ChangeTransition(_attackChoices, "A1 Choice", "BEAM SWEEP L", "Orb Wait");
			FsmutilExt.ChangeTransition(_attackChoices, "A1 Choice", "BEAM SWEEP R", "Eye Beam Wait");
		}
		if (_attackChoices.FsmVariables.GetFsmInt("Arena").Value == 2 && !arena2Set)
		{
			Modding.Logger.Log("[Ultimatum Radiance] Starting Phase 2");
			arena2Set = true;
			FsmutilExt.GetAction<SetIntValue>(_attackCommands, "Orb Antic", 1).intValue = 12;
			FsmutilExt.GetAction<RandomInt>(_attackCommands, "Orb Antic", 2).min = 10;
			FsmutilExt.GetAction<RandomInt>(_attackCommands, "Orb Antic", 2).max = 14;
			FsmutilExt.GetAction<Wait>(_attackCommands, "Orb Summon", 2).time = 0.1f;
			FsmutilExt.GetAction<SetPosition>(_beamsweepercontrol, "Beam Sweep L", 3).x = 89f;
			FsmutilExt.GetAction<iTweenMoveBy>(_beamsweepercontrol, "Beam Sweep L", 5).vector = new Vector3(-75f, 0f, 0f);
			FsmutilExt.GetAction<iTweenMoveBy>(_beamsweepercontrol, "Beam Sweep L", 5).time = 3f;
			FsmutilExt.GetAction<SetPosition>(_beamsweepercontrol, "Beam Sweep R", 4).x = 32.6f;
			FsmutilExt.GetAction<iTweenMoveBy>(_beamsweepercontrol, "Beam Sweep R", 6).vector = new Vector3(75f, 0f, 0f);
			FsmutilExt.GetAction<iTweenMoveBy>(_beamsweepercontrol, "Beam Sweep R", 6).time = 3f;
			FsmutilExt.GetAction<SetPosition>(_beamsweeper2control, "Beam Sweep L", 2).x = 89f;
			FsmutilExt.GetAction<iTweenMoveBy>(_beamsweeper2control, "Beam Sweep L", 4).vector = new Vector3(-75f, 0f, 0f);
			FsmutilExt.GetAction<iTweenMoveBy>(_beamsweeper2control, "Beam Sweep L", 4).time = 3f;
			FsmutilExt.GetAction<SetPosition>(_beamsweeper2control, "Beam Sweep R", 3).x = 32.6f;
			FsmutilExt.GetAction<iTweenMoveBy>(_beamsweeper2control, "Beam Sweep R", 5).vector = new Vector3(75f, 0f, 0f);
			FsmutilExt.GetAction<iTweenMoveBy>(_beamsweeper2control, "Beam Sweep R", 5).time = 3f;
			FsmutilExt.GetAction<SetIntValue>(_attackCommands2, "Orb Antic", 1).intValue = 12;
			FsmutilExt.GetAction<RandomInt>(_attackCommands2, "Orb Antic", 2).min = 10;
			FsmutilExt.GetAction<RandomInt>(_attackCommands2, "Orb Antic", 2).max = 14;
			FsmutilExt.GetAction<Wait>(_attackCommands2, "Orb Summon", 2).time = 0.1f;
			FsmutilExt.GetAction<SetPosition>(_beamsweeper3control, "Beam Sweep L", 3).x = 89f;
			FsmutilExt.GetAction<iTweenMoveBy>(_beamsweeper3control, "Beam Sweep L", 5).vector = new Vector3(-75f, 0f, 0f);
			FsmutilExt.GetAction<iTweenMoveBy>(_beamsweeper3control, "Beam Sweep L", 5).time = 3f;
			FsmutilExt.GetAction<SetPosition>(_beamsweeper3control, "Beam Sweep R", 4).x = 32.6f;
			FsmutilExt.GetAction<iTweenMoveBy>(_beamsweeper3control, "Beam Sweep R", 6).vector = new Vector3(75f, 0f, 0f);
			FsmutilExt.GetAction<iTweenMoveBy>(_beamsweeper3control, "Beam Sweep R", 6).time = 3f;
			FsmutilExt.GetAction<SetPosition>(_beamsweeper4control, "Beam Sweep L", 2).x = 89f;
			FsmutilExt.GetAction<iTweenMoveBy>(_beamsweeper4control, "Beam Sweep L", 4).vector = new Vector3(-75f, 0f, 0f);
			FsmutilExt.GetAction<iTweenMoveBy>(_beamsweeper4control, "Beam Sweep L", 4).time = 3f;
			FsmutilExt.GetAction<SetPosition>(_beamsweeper4control, "Beam Sweep R", 3).x = 32.6f;
			FsmutilExt.GetAction<iTweenMoveBy>(_beamsweeper4control, "Beam Sweep R", 5).vector = new Vector3(75f, 0f, 0f);
			FsmutilExt.GetAction<iTweenMoveBy>(_beamsweeper4control, "Beam Sweep R", 5).time = 3f;
		}
		if (!(abs.transform.position.y >= 150f))
		{
			return;
		}
		if (damageDealt >= 7000)
		{
			GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat").ChangeState(GetFsmEventByName(GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat"), "SLOW VANISH"));
			if (!onePlatSet)
			{
				onePlatSet = true;
				Log("Removing upper right platform");
				FsmutilExt.GetAction<Wait>(_attackCommands, "Orb Antic", 0).time = 0.01f;
				FsmutilExt.GetAction<SetIntValue>(_attackCommands, "Orb Antic", 1).intValue = 5;
				FsmutilExt.GetAction<RandomInt>(_attackCommands, "Orb Antic", 2).min = 4;
				FsmutilExt.GetAction<RandomInt>(_attackCommands, "Orb Antic", 2).max = 6;
				FsmutilExt.GetAction<Wait>(_attackCommands, "Orb Summon", 2).time = 0.01f;
				FsmutilExt.GetAction<Wait>(_attackCommands, "Orb Pause", 0).time = 0.01f;
				FsmutilExt.GetAction<Wait>(_attackChoices, "Orb Recover", 0).time = 0.1f;
			}
		}
		if (damageDealt >= 8000)
		{
			_spikeClone.LocateMyFSM("Control").SendEvent("UP");
			_spikeClone2.LocateMyFSM("Control").SendEvent("UP");
			_spikeClone3.LocateMyFSM("Control").SendEvent("UP");
			_spikeClone4.LocateMyFSM("Control").SendEvent("UP");
			_spikeClone5.LocateMyFSM("Control").SendEvent("UP");
			if (!platSpikesSet)
			{
				platSpikesSet = true;
				GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat").ChangeState(GetFsmEventByName(GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat"), "SLOW VANISH"));
			}
		}
	}

	private void AddNailWall(string stateName, string eventName, float delay, int index)
	{
		FsmutilExt.InsertAction(_attackChoices, stateName, (FsmStateAction)new SendEventByName
		{
			eventTarget = FsmutilExt.GetAction<SendEventByName>(_attackChoices, "Nail L Sweep", 0).eventTarget,
			sendEvent = eventName,
			delay = delay,
			everyFrame = false
		}, index);
	}

	private void AddNailWall2(string stateName, string eventName, float delay, int index)
	{
		FsmutilExt.InsertAction(_attackChoices2, stateName, (FsmStateAction)new SendEventByName
		{
			eventTarget = FsmutilExt.GetAction<SendEventByName>(_attackChoices2, "Nail L Sweep", 0).eventTarget,
			sendEvent = eventName,
			delay = delay,
			everyFrame = false
		}, index);
	}

	private static FsmEvent GetFsmEventByName(PlayMakerFSM fsm, string eventName)
	{
		FsmEvent[] fsmEvents = fsm.FsmEvents;
		foreach (FsmEvent fsmEvent in fsmEvents)
		{
			if (fsmEvent.Name == eventName)
			{
				return fsmEvent;
			}
		}
		return null;
	}

	private static void Log(object obj)
	{
		Modding.Logger.Log("[Ultimatum Radiance] " + obj);
	}

	private void OnDestroy() {
		On.HealthManager.TakeDamage -= KnightHit;
		damageDealt = 0;
		flagP2 = false;
		flagP3 = false;
		flagP4 = false;
		flagP5 = false;
		flagDie = false;
		flag2 = false;
		endFlag = false;
	}
}
