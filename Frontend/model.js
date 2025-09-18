// model.js
const model = {
  app: { currentPage: 'default' },

  data: {
    // Life Points (LP)
    points: Number(localStorage.getItem('stb:points')) || 0,
    // Sharpness level (0..MAX_SHARPNESS)
    sharpness: Number(localStorage.getItem('stb:sharpness')) || 0,
    // Overflow when LP is capped
    whetstones: Number(localStorage.getItem('stb:whetstones')) || 0,
    // Per-day target completions: { "YYYY-MM-DD": { gym: true, ... } }
    completions: JSON.parse(localStorage.getItem('stb:completions') || '{}'),
  },

  consts: {
    LP_MAX: 60,           // LP hard cap
    LP_PER_TARGET: 20,    // award per target
    SHARPEN_COST: 20,     // cost per sharpen
    MAX_SHARPNESS: 3,     // 0..3 (Dull, Sharp, Sharper, Super Sharp)
  },

  // ---- persistence ----
  save() {
    localStorage.setItem('stb:points', this.data.points);
    localStorage.setItem('stb:sharpness', this.data.sharpness);
    localStorage.setItem('stb:whetstones', this.data.whetstones);
    localStorage.setItem('stb:completions', JSON.stringify(this.data.completions));
  },

  // ---- helpers ----
  todayKey() { return new Date().toISOString().slice(0, 10); }, // YYYY-MM-DD
};

window.model = model;
