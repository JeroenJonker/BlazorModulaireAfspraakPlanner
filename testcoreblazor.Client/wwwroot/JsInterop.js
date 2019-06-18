var LastElementDOM;
var LastSelectedList;

window.JsFunctions = {
  initializeSelect2: function() {
    // $('select').select2();
    $('.select-multiple-option-type-component').select2({
      placeholder: "Selecteer uw keuzes"
    });
  },
  getSelectedList: function(multiSelect) {
    var selectedList = [];
  
    for (let i = 0; i < multiSelect.length; i++) {
      if (multiSelect[i].selected) {
        selectedList.push(multiSelect[i].value);
      }
    }

    return selectedList;
  }
}

function mutateDOM(ElementDOM, selectedList) {
  if (LastElementDOM == ElementDOM && arraysEqual(selectedList, LastSelectedList))
    return false;

  LastElementDOM = ElementDOM;
  LastSelectedList = selectedList;

  var event = new Event('change');
  ElementDOM.dispatchEvent(event);
}

function selectChange(_this) {
  var selectedList = [];
  
  for (let i = 0; i < _this.length; i++) {
    if (_this[i].selected) {
      selectedList.push(_this[i].value);
    }
  }

  mutateDOM(_this, selectedList);
}

function arraysEqual(a, b) {
  if (a === b) return true;
  if (a == null || b == null) return false;
  if (a.length != b.length) return false;

  for (var i = 0; i < a.length; ++i) {
    if (a[i] !== b[i]) return false;
  }
  return true;
}