(function () {
    const t = {};


  
  t.isDone = function isDone(id) {
    const d = model.todayKey();
    return !!(model.data.completions[d] && model.data.completions[d][id]);
  };

  
  t.markDone = function markDone(id) {
    const d = model.todayKey();
    (model.data.completions[d] ||= {})[id] = true;
    model.save();
  };

  
  t.complete = function complete(id) {
    if (t.isDone(id)) return { ok:false, reason:'already' };
    t.markDone(id);
    return { ok:true, ...controller.addPointsWithOverflow(model.consts.LP_PER_TARGET) };
  };

  
  t.handleClick = function handleClick(btnEl) {
    const id = btnEl?.dataset?.id;
    if (!id) return;
    const res = t.complete(id);
    if (!res.ok) return;

    
    btnEl.disabled = true;
    btnEl.classList.add('is-done');
    if (!btnEl.textContent.includes('✓')) btnEl.textContent += ' ✓';

    controller.updateHeaderUI();
  };

  window.targetsController = t;
})();
