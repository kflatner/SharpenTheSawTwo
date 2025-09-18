function visitBlacksmithView(){
  const s = model.data.sharpness | 0;
  const atMax = s >= model.consts.MAX_SHARPNESS;
  const can = blacksmithController.canSharpen() && !atMax;
  const pct = blacksmithController.levelPercent();

  return /*html*/`
    <div class="blacksmith-container">
      <h2>The blacksmith can sharpen your blade for some points</h2>
      <p>Life Points: <strong>${model.data.points|0}</strong></p>
      <p>Sharpness: <strong>${s}</strong> / ${model.consts.MAX_SHARPNESS}</p>

      <div class="meter" style="margin:8px 0 14px"><span style="width:${pct}%"></span></div>

      <button class="btn" onclick="blacksmithController.handleSharpen()" ${can ? '' : 'disabled'}>
        ${atMax ? 'Max sharpness reached' : `Sharpen the blade — ${model.consts.SHARPEN_COST} LP`}
      </button>

      <p class="dim" id="smithMsg" style="margin-top:10px">
        ${atMax ? 'You are Super Sharp!' : (can ? '' : 'Not enough Life Points.')}
      </p>
    </div>`;
}
