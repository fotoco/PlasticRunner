using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public static float acceleration = 10.0f;
    public static float speed_min = 4.0f;
    public static float speed_max = 8.0f;
    public static float jump_height_max = 3.0f;
    public static float jump_key_release_reduce = 0.5f;
    public static float naraku_height = -5.0f;

    public enum STEP
    {
        none = -1,
        run = 0,
        jump,
        miss,
        num,
    };

    public STEP step = STEP.none;
    public STEP next_step = STEP.none;

    public float step_timer = 0.0f;
    private bool is_landed = false;
    private bool is_colided = false;
    private bool is_key_released = false;



	// Use this for initialization
	void Start () {
        this.next_step = STEP.run;
	}
	
	// Update is called once per frame
	void Update () {
        //this.transform.Translate(new Vector3(0.0f, 0.0f, 3.0f * Time.deltaTime));
        //this.transform.Translate(new Vector3(3.0f * Time.deltaTime, 0.0f, 0.0f));

        Vector3 velocity = this.GetComponent<Rigidbody>().velocity;
        this.check_landed();


        switch(this.step)
        {
            case STEP.run:
            case STEP.jump:
                if(this.transform.position.y < naraku_height)
                {
                    this.next_step = STEP.miss;
                }
                break;
        }



        this.step_timer += Time.deltaTime;

        if(this.next_step == STEP.none)
        {
            switch(this.step)
            {
                case STEP.run:
                    if(!this.is_landed)
                    {

                    }
                    else
                    {
                        if(Input.GetMouseButtonDown(0))
                        {
                            this.next_step = STEP.jump;
                        }
                    }
                    break;
                case STEP.jump:
                    if(this.is_landed)
                    {
                        this.next_step = STEP.run;
                    }
                    break;
            }
        }

        while(this.next_step != STEP.none)
        {
            this.step = this.next_step;
            this.next_step = STEP.none;
            
            switch(this.step)
            {
                case STEP.jump:
                    velocity.y = Mathf.Sqrt(2.0f * 9.8f * PlayerControl.jump_height_max);
                    this.is_key_released = false;
                    break;
            }

            this.step_timer = 0.0f;
        }


        switch(this.step)
        {
            case STEP.run:
                velocity.x += PlayerControl.acceleration * Time.deltaTime;

                if(Mathf.Abs(velocity.x) > PlayerControl.speed_max)
                {
                    velocity.x *= PlayerControl.speed_max / Mathf.Abs(this.GetComponent<Rigidbody>().velocity.x);
                }
                break;
            case STEP.jump:
                do
                {
                    if (!Input.GetMouseButtonUp(0))
                    {
                        break;
                    }

                    if (this.is_key_released)
                    {
                        break;
                    }

                    if(velocity.y <= 0.0f)
                    {
                        break;
                    }

                    velocity.y *= jump_key_release_reduce;

                    this.is_key_released = true;

                } while (false);
                break;
            case STEP.miss:
                velocity.x -= PlayerControl.acceleration * Time.deltaTime;
                if(velocity.x < 0.0f)
                {
                    velocity.x = 0.0f;
                }
                break;

        }

        this.GetComponent<Rigidbody>().velocity = velocity;



    }

    private void check_landed()
    {
        this.is_landed = false;

        do
        {
            Vector3 s = this.transform.position;
            Vector3 e = s + Vector3.down * 1.0f;

            RaycastHit hit;
            if (!Physics.Linecast(s, e, out hit))
            {
                break;
            }

            if (this.step == STEP.jump)
            {
                if (this.step_timer < Time.deltaTime * 3.0f)
                {
                    break;
                }
            }

            this.is_landed = true;

        } while (false);
    }
}
