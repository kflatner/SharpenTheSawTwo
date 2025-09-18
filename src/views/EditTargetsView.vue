<template>
    <div v-if="selected == `` " class="center">
            <h2>You can edit targets here</h2>
            <button style="margin: 60px; font-size: larger;" v-on:click="selectOption('newTarget')">Add new target</button>
            <button style="font-size: larger;" v-on:click="selectOption('editTargets')">Edit existing targets</button>
    </div>

    <div v-else-if="selected == 'newTarget'" class="center">
        <form>
            <label>Name:</label>
            <input type="text" required v-model="inputForms.name">
        <br>
            <label>Text:</label>
            <input type="text" required v-model="inputForms.text">
        <br>
            <button v-on:click="createNewTarget">Create New</button>
        </form>
        {{ inputForms.name }}
        {{ inputForms.text }}
    </div>

       <div v-else-if="selected == 'editTargets'" class="center">
        <h2>Select which one to edit</h2>
        <div v-html="Button()"></div>
    </div>
    
</template>

<script>
export default {
    data() {
        return {
            selected: ``,
            inputForms: {
                name: '',
                text: ''
            }
        };
    },
    created () {
    },
    methods: {
      selectOption(option) {
        this.selected = option
      },
      createNewTarget() {
        this.selected = ``,
        this.SendToAPI(this.inputForms)
      },
      SendToAPI(x) {
        console.log(x)
      },
      Button() {
        var Tasks = [
  {
    "Name": "gym",
    "Label": "Went to Gym",
  },
  {
    "Name": "pomodoro",
    "Label": "Did two hours of Pomodoro",
  },
  {
    "Name": "lunch",
    "Label": "Finished the lunch without noise!",
  },
  {
    "Name": "walk",
    "Label": "Went for a walk",
  },
  {
    "Name": "learned",
    "Label": "Learned something new!",
  }
];
      var Buttons = "";
      Tasks.forEach(element => {
        Buttons += `<button style="margin-right: 5px;" class="btn target-btn}"
                    data-id="${element.Name}"
                    onclick="editTarget(${element.Name})">
              ${element.Label}
            </button>`
      });
        return Buttons
      }
    }   
}


</script>