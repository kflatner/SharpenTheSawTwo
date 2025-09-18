function targetsView(){
  const btn = (id, label) => {
    const done = targetsController.isDone(id);
    return `<button class="btn target-btn${done ? ' is-done' : ''}"
                    data-id="${id}"
                    ${done ? 'disabled' : ''}
                    onclick="targetsController.handleClick(this)">
              ${label}${done ? ' ✓' : ''}
            </button>`;
  };

  return /*html*/`
    <div class="targets-container">
      <h2>When done with your task, click the button and get your points!</h2>
      ${btn('gym', 'Went to Gym')}
      ${btn('pomodoro', 'Did two hours of Pomodoro')}
      ${btn('lunch', 'Finished the lunch without noise!')}
      ${btn('walk', 'Went for a walk')}
      ${btn('learned', 'Learned something new!')}
     
      <p class="dim" style="margin-top:10px">
        Each target awards <strong>+${model.consts.LP_PER_TARGET} LP</strong> (LP capped at ${model.consts.LP_MAX}; overflow becomes Whetstones).
      </p>
    </div>`;
}
