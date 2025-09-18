<template>
    <div class="targets-container">
      <h2>When done with your task, click the button and get your points!</h2>
      <div v-if="Tasks.length != 0" v-for="element in this.Tasks">
          <button
                    v-on:click="updateStatusTask(element.id, element)" 
                    style="margin-right: 5px;" class="btn target-btn" :class="element.status ? `is-done` : ``"
                    data-id="{{element.id}}"
                    :disabled="element.status"
                    >
              {{element.title}}{{element.status ? '✓' : ''}}
            </button>
</div>
     
      <p class="dim" style="margin-top:10px">
        Each target awards <strong>+{{ LifePointPerTarget }} LP</strong> (LP capped at {{ LifePointsMax }}; overflow becomes Whetstones).
      </p>
    </div>
    <button v-on:click="getTasks"></button>
</template>

<script>
import { http } from '../api/http';
import { mapState } from 'vuex'

export default {
    data() {
        return {
          LifePointPerTarget: 20,
          LifePointsMax: 60, 
          Tasks: []
        };
    },
    created () {
    }, 
    computed: {
        ...mapState(['loggedInn'])
      },
    methods: {
      async getTasks() {
          let url = '/getTasks/' + this.loggedInn.id;
          const res = await http.get(url, this.loggedInn.id);
        this.Tasks = res.data;
         
        
      
      },
      async updateStatusTask(id, task) {
        let url = '/updateTask/' + id;
        task.title = "";
        task.status = !task.status;
        console.log(task)
        let x = await http.post(url, task);
        console.log(x)
        this.getTasks()
      }
    }

}
</script>