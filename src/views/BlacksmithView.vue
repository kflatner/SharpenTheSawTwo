<template>
    <div class="blacksmith-picture">
    </div>
    <div class="blacksmith-container">
      <h2>The blacksmith can sharpen your blade for some points</h2>
      <p>Life Points: <strong>{{ lifePoints }}</strong></p>
      <p>Sharpness: <strong>{{ sharpenedAmount }}</strong> / {{ sharpenedMax }}</p>

      <div class="meter" style="margin:8px 0 14px"><span :style="{'width': sharpenedPercent + '%'}"></span></div>

      <button class="btn" :disabled="!canSharpen">
        {{canSharpen ? "Sharpen the blade — 20 LP" : "Max sharpness reached" }}
      </button>

      <p class="dim" id="smithMsg" style="margin-top:10px">
        {{canSharpen ? lifePoints >= sharpenedCost ? '' : 'Not enough Life Points.' : 'You are Super Sharp!' }}
      </p>
    </div>
</template>

<style>
#app[data-view="blacksmith"]{
  --view-bg: linear-gradient(180deg, rgba(255,255,255,.08), rgba(255,255,255,.04));
  position: relative; /* stacking context for ::before and content */
}

/* Put the image as a hero banner inside the card, above content flow */
.blacksmith-picture {
   content:"";
  display:block;
  height: clamp(180px, 35vw, 360px);          /* hero size */
  background: url('../assets/blacksmith.png') center no-repeat;
  background-size: contain;                    /* show full image */
  margin: -26px -22px 16px;                    /* bleed to card edges (matches #app padding) */
  border-radius: calc(var(--radius) - 6px);
  box-shadow: inset 0 -60px 80px rgba(0,0,0,.25); /* subtle bottom fade */
  position: relative;
  z-index: 0;
}


</style>

<script>
export default {
    data() {
        return {
          lifePoints: null,
          sharpenedAmount: null,
          sharpenedMax: null,
          sharpenedPercent: null,
          canSharpen: true, 
          sharpenedCost: null
        };
    },
    created () {
      this.getVariables()
    },
    methods: {
      getVariables() {
        "Remember to put so dynamic"
        this.lifePoints = 20,
        this.sharpenedAmount = 2,
        this.sharpenedMax = 3,
        this.sharpenedPercent = 66,
        this.canSharpen = true,
        this.sharpenedCost = 20
      }
    }

}
</script>