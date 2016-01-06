using UnityEngine;

namespace UGUICustom
{
	/// <summary>
	/// インスペクター配列にUIMotionを継承したスクリプトをアサインし、アニメーターからindexを指定する事で
	/// １つのアニメーターから異なるオブジェクトのスクリプトモーションを再生させるStrategy context的なクラスです.
	/// InterfaceにUIMotion.cs、実装にUIMotionサブクラス、コンテキストとして当クラスのSerializeField参照と実行用ExecuteUIMotionソッドとしています.
	/// 実行はアニメーターからされます.当クラスはAnimatorコンポーネントが入っているオブジェクトにアタッチして使用して下さい.
	/// </summary>
	[AddComponentMenu("UI/Motion/UIMotionPlayer")]
	[RequireComponent(typeof(Animator))]
	public class UIMotionPlayer : MonoBehaviour
	{
		[SerializeField] private UIMotion[] _uiMotion;

		/// <summary>
		/// アニメーターからの実行想定です。インスペクターに入っているIndexを引数に指定して実行して下さい..
		/// </summary>
		/// <param name="index">Index.</param>
		public void ExecuteUIMotion(int index)
		{
			_uiMotion[index].PlayUIMotion();
		}
	}
}