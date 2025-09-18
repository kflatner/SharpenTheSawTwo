

(function () {
  const b = {};

  b.canSharpen = function canSharpen() {
    return model.data.points >= model.consts.SHARPEN_COST &&
           model.data.sharpness < model.consts.MAX_SHARPNESS;
  };

 
  b.sharpenOnce = function sharpenOnce() {
    if (model.data.sharpness >= model.consts.MAX_SHARPNESS) {
      return { ok:false, reason:'max' };
    }
    if (model.data.points < model.consts.SHARPEN_COST) {
      return { ok:false, reason:'points' };
    }
    model.data.points -= model.consts.SHARPEN_COST;
    model.data.sharpness += 1;
    model.save();
    return { ok:true };
  };

 
  b.levelPercent = function levelPercent() {
    return Math.min((model.data.sharpness / model.consts.MAX_SHARPNESS) * 100, 100);
  };

  
  b.handleSharpen = function handleSharpen() {
    const res = b.sharpenOnce();
    const msg = document.getElementById('smithMsg');
    if (!res.ok) {
      if (msg) msg.textContent = (res.reason === 'max')
        ? 'Already at max sharpness.'
        : 'Not enough Life Points.';
      return;
    }
    if (typeof updateView === 'function') updateView();        
    controller.updateHeaderUI();                               
  };

  window.blacksmithController = b;
})();
