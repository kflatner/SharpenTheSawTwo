(function () {
    const c = {};


  /* ============== NAVIGATION ============== */
 
  c.goto = function goto(viewName) {
    model.app.currentPage = viewName || 'default';
    if (typeof updateView === 'function') updateView();
    return model.app.currentPage;
  };

  
  
  c.addPointsWithOverflow = function addPointsWithOverflow(amount) {
    const n = Number(amount) || 0;
    const room = Math.max(0, model.consts.LP_MAX - model.data.points);
    const toLP = Math.min(n, room);
    const overflow = n - toLP;

    model.data.points += toLP;
    model.data.whetstones += overflow;
    model.save();

    return { toLP, overflow, lp: model.data.points, ws: model.data.whetstones };
  };

  
  c.sharpTierLabel = function sharpTierLabel(s) {
    const i = Math.min(Math.max(s|0, 0), model.consts.MAX_SHARPNESS);
    return ['Dull','Sharp','Sharper','Super Sharp'][i];
  };

  c.sharpIconForLevel = function sharpIconForLevel(s) {
    const i = Math.min(Math.max(s|0, 0), model.consts.MAX_SHARPNESS);
    const ICONS = [
      './images/saw_super_dull.png',   // 0
      './images/saw_dull.png',  // 1
      './images/saw_sharp.png',  // 2 
      './images/saw_super.png'   // 3
    ];
    return ICONS[i];
  };

  
  c.updateHeaderUI = function updateHeaderUI() {
    const lp = model.data.points | 0;
    const ws = model.data.whetstones | 0;
    const s  = model.data.sharpness | 0;

    
    const badgeLP = document.getElementById('pointsBadge');
    if (badgeLP) badgeLP.textContent = lp + ' LP';

    const badgeWS = document.getElementById('wsBadge');
    if (badgeWS) badgeWS.textContent = ws + ' WS';

    
    const icon = document.getElementById('sharpIcon');
    if (icon) {
      icon.src   = c.sharpIconForLevel(s);
      icon.alt   = 'Blade sharpness ' + s;
      icon.title = 'Sharpness: ' + s + ' (' + c.sharpTierLabel(s) + ')';
      icon.classList.toggle('very-sharp', s >= model.consts.MAX_SHARPNESS);
    }

    
    const label = document.getElementById('sharpLabel');
    if (label) label.textContent = c.sharpTierLabel(s);

    const segs = document.querySelectorAll('#sharpMeter .seg');
    segs.forEach(seg => {
      const th = Number(seg.dataset.th); // 1,2,3
      seg.classList.toggle('active', s >= th);
    });

    c.tryCelebrateSharpness3(s, icon);
  };

  
  c.tryCelebrateSharpness3 = function tryCelebrateSharpness3(s, iconEl) {
    const KEY = 'stb:celebrated_s3';
    if (s >= 3 && !localStorage.getItem(KEY)) {
      localStorage.setItem(KEY, '1');
      if (iconEl) {
        iconEl.classList.add('celebrate');
        setTimeout(() => iconEl.classList.remove('celebrate'), 900);
      }
      const meter = document.getElementById('sharpMeter');
      if (meter) {
        meter.classList.add('celebrate');
        setTimeout(() => meter.classList.remove('celebrate'), 900);
      }
      c.showAchievement('Great job! Super Sharp unlocked!');
    }
  };

  c.showAchievement = function showAchievement(text) {
    const t = document.createElement('div');
    t.className = 'achieve-toast';
    t.textContent = text;
    document.body.appendChild(t);
    setTimeout(() => t.remove(), 3000);
  };

  
  window.controller = c;
})();
