using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace UGUICustom
{
	/// <summary>
	/// 文字がパタパタと更新されながら確定される演出です.
	/// </summary>
	[AddComponentMenu("UI/Motion/RandomTextMotion")]
	[RequireComponent(typeof(Text))]
	public class UIRandomTextMotion : UIMotion
	{
		private Text        _textComponent;
		private string      _originalText;
		private string      _motionText;
		private string      _randomCharacter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_*%$@^&#()+}{:>";
		private bool        _isMotion        = false;
		private List<float> _randomTimerList = new List<float>();

		[Tooltip("ランダムモーションさせる時間")]
		[SerializeField] private float _duration = 0.5f;
		[Tooltip("モーションスタート時の文字")]
		[SerializeField] private string _motionStartText = "_";
		[Tooltip("モーションの激しさを和らげる係数。大きい程緩やかになります")]
		[SerializeField] private int _laziness = 3;

		private void _Initialize ()
		{
			_textComponent = GetComponent<Text>();
			_originalText  = _textComponent.text;
			_MakeRandomTimerList();
			_SetMotionStartText();
		}

		/// <summary>
		/// 文字毎の、徐々に数値が大きくなるランダムなタイマーリストを作成.
		/// </summary>
		private void _MakeRandomTimerList ()
		{
			_randomTimerList.Clear();
			for(int i = 0; i < _originalText.Length; i++)
			{
				float rate = i / _originalText.Length;
				_randomTimerList.Add(Random.Range(rate,1));
			}
		}

		/// <summary>
		/// モーションスタート時の文字を設定.
		/// </summary>
		private void _SetMotionStartText ()
		{
			_textComponent.text = "";
			for(int i = 0; i < _originalText.Length; i++)
			{
				_textComponent.text += _motionStartText;
			}
		}

		/// <summary>
		/// durationの間、文字をランダムに変更していきます.
		/// </summary>
		/// <returns>The random text motion coroutine.</returns>
		private IEnumerator _RandomTextMotionCoroutine ()
		{
			float _timer = 0f;
			float _percent;

			_isMotion = true;

			while(_timer <= _duration)
			{
				_timer += Time.deltaTime;
				//タイマー内経過時間の割合.
				_percent = _timer / _duration;
				_motionText = "";
				for (int i = 0; i < _originalText.Length; i++)
				{
					//タイマーリストより数値が経過していたら文字確定.
					if (_percent >= _randomTimerList[i])
					{
						_motionText += _originalText.Substring(i,1);
					}
					//変化まで余裕がある文字の変化は遅らせる.
					else if (_percent < _randomTimerList[i] / _laziness)
					{
						_motionText += "-";
					}
					else
					{
						_motionText += _randomCharacter.Substring(Random.Range(0,_randomCharacter.Length - 1),1);
					}
				}
				_textComponent.text = _motionText;
				yield return null;
			}
			_isMotion = false;
			_textComponent.text = _originalText;
		}

		/// <summary>
		/// 実行メソッド。UIMotion.csのインターフェース.
		/// </summary>
		public override void PlayUIMotion ()
		{
			if (_isMotion)
			{
				return;
			}
			_Initialize();
			StartCoroutine("_RandomTextMotionCoroutine");
		}
	}
}