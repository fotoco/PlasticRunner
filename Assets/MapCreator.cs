using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour {

    public static float block_width = 1.0f;
    public static float block_height = 0.2f;
    public static int block_num_in_screen = 24;

    private struct FloorBlock
    {
        public bool is_created;
        public Vector3 position;
    };

    private FloorBlock last_block;
    private PlayerControl player = null;
    private BlockCreator block_creator;



	// Use this for initialization
	void Start () {
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        this.last_block.is_created = false;
        this.block_creator = this.gameObject.GetComponent<BlockCreator>();
	}
	
	// Update is called once per frame
	void Update () {
        float block_generate_x = this.player.transform.position.x;
        block_generate_x += block_width * ((float)block_num_in_screen + 1) / 2.0f;

        while(this.last_block.position.x < block_generate_x)
        {
            this.create_floor_block();
        }
	}

    private void create_floor_block()
    {
        Vector3 block_position;
        if(!this.last_block.is_created)
        {
            block_position = this.player.transform.position;
            block_position.x -= block_width * ((float)block_num_in_screen / 2.0f);
            block_position.y = 0.0f;

        }
        else
        {
            block_position = this.last_block.position;

        }

        block_position.x += block_width;
        this.block_creator.createBlock(block_position);

        this.last_block.position = block_position;
        this.last_block.is_created = true;

    }

    public bool isDelete(GameObject block_object)
    {
        bool ret = false;

        float left_limit = this.player.transform.position.x -
            block_width * ((float)block_num_in_screen / 2.0f);

        if(block_object.transform.position.x < left_limit)
        {
            ret = true;
        }

        return ret;
    }
}
