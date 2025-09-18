function updateView() {
  let currentView = '';

  switch (model.app.currentPage) {
    case 'target':
      currentView = targetsView();
      break;
    case 'blacksmith':
      currentView = visitBlacksmithView();
      break;
    case 'nextActivites':
      currentView = (typeof nextActivityView === 'function')
        ? nextActivityView()
        : '<p class="dim">No next activity view.</p>';
      break;
    case 'boostme':
      currentView = boostView();
      break;
    default:
      currentView = defaultView();
      break;
  }

  const el = document.getElementById('app');
  if (!el) return;
  el.innerHTML = currentView;

  
  el.setAttribute('data-view', model.app.currentPage);

 
  controller.updateHeaderUI();
}
window.updateView = updateView;


