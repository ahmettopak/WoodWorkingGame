using UnityEngine ;

public class Knife : MonoBehaviour {
   [SerializeField] private float movementSpeed ;
   [SerializeField] private float hitDamage ;
   [SerializeField] private Wood wood ;

   [SerializeField] private ParticleSystem woodFx ;

   private ParticleSystem.EmissionModule woodFxEmission ;

   private Rigidbody knifeRb ;
   private Vector3 movementVector ;
   private bool isMoving = false ;

   public AudioSource audio_s;
   bool play_sound;

  private Vector3 mOffset;
  private float mZCoord;

   private void Start () {
      knifeRb = GetComponent<Rigidbody> () ;

      woodFxEmission = woodFx.emission ;
      play_sound=true;
      
   }

   private void Update () {
     
      
   }

   private void OnCollisionExit (Collision collision) {
      woodFxEmission.enabled = false ;
      play_sound=true;
      audio_s.Stop();
   }

   private void OnCollisionStay (Collision collision) {
      Coll coll = collision.collider.GetComponent <Coll> () ;
      if (coll != null) {
         // hit Collider:
         woodFxEmission.enabled = true ;
         if(play_sound)
         {
            play_sound=false;
            audio_s.Play();
         }
         
         woodFx.transform.position = collision.contacts [ 0 ].point ;

         coll.HitCollider (hitDamage) ;
         wood.Hit (coll.index, hitDamage) ;
      }
   }

   void OnMouseDown()
    {
      mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
      mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
    }
    private Vector3 GetMouseAsWorldPoint()
    {
      Vector3 mousePoint = Input.mousePosition;
      mousePoint.z = mZCoord;
      return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    void OnMouseDrag()
    {     
      transform.position = GetMouseAsWorldPoint() + mOffset;

      if(transform.position.y>2.1f)
       transform.position=new Vector3(transform.position.x , 2.1f , transform.position.z);
    }
}
