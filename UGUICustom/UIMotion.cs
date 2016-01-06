using UnityEngine;

namespace UGUICustom
{
	/// <summary>
	/// UIMotionPlayer.csでUIMotionを継承した各種UIの動きを再生する為の簡易なStrategyパターンのInterfaceです.
	/// Interfaceがインスペクターに表示出来ない為、抽象クラスインターフェースにしています.
	/// Interfaceに当クラス、実装にUIMotionサブクラス、ContextとしてUIMotionPlayer.csのSerializeField参照と実行用ExecuteUIMotionメソッド.
	/// 実行はアニメーターからされます.
	/// </summary>
	public abstract class UIMotion : MonoBehaviour
	{
		public abstract void PlayUIMotion();
	}
}