<template>
    <div class="targets-container">
      <h2>When done with your task, click the button and get your points!</h2>
      <div v-if="Tasks.length !== 0" v-html="Button()"></div>
     
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
      Button() {
       
      var Buttons = "";
      Tasks.forEach(element => {
        Buttons += `<button style="margin-right: 5px;" class="btn target-btn${element.Done ? ' is-done' : ''}"
                    data-id="${element.Name}"
                    ${element.Done ? 'disabled' : ''}
                    onclick="targetsController.handleClick(this)">
              ${element.Label}${element.Done ? ' ✓' : ''}
            </button>`
      });
        return Buttons
      },
      async getTasks() {
          let url = '/getTasks/' + this.loggedInn.id;
          console.log(url)
          const res = await http.get(url, this.loggedInn.id);
        let data = res.data;
        console.log(data)
      
      }
    }

}
</script>