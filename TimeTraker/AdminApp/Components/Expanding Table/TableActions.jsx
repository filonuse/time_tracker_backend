import styles from './tableStyle.css'

// Function provides the functionality of expanding and collapsing the row with additional information.
export function expandingRow(event){
    const element = event.target.closest('tr');
    const expansibleElement = element.nextElementSibling.querySelector('.'+styles.expansible_container);


    if(element.dataset.state.match(/collapsed/)){
        openRow(element, expansibleElement)
    }
    else {
        closeRow(element, expansibleElement)
    }

    ToggleIndicator();
}

// Function provides the functionality of the expansion indicator ("+") on the header of table when last is clicked.
export function onIndicatorClick(event){
    const theadIndicator = event.target.closest('th');
    const elements = document.querySelectorAll('tr[data-state="expanded"]');

    if (elements.length){
        elements.forEach(element => {
            const expansibleElement = element.nextElementSibling.querySelector('.'+styles.expansible_container);

            closeRow(element, expansibleElement);
        });
    }
    else {
        document.querySelectorAll('tr[data-state="collapsed"]').forEach(element => {
            const expansibleElement = element
                .nextElementSibling
                .querySelector('.'+styles.expansible_container);

            openRow(element, expansibleElement);
        });
    }

    ToggleIndicator();
}

// Additional function.
// It opens the row with additional information.
function openRow(element, expansibleElement) {
    let rowHeight = 0;
    expansibleElement.childNodes.forEach(node => rowHeight += node.clientHeight);

    element.dataset.state = 'expanded';
    element.querySelector('.md').className = 'md md-remove';
    expansibleElement.style.height = rowHeight + 'px';
}

// Additional function.
// It closes the row with additional information.
function closeRow(element, expansibleElement) {
    element.dataset.state = 'collapsed';
    element.querySelector('.md').className = 'md md-add';
    expansibleElement.style.height = '0';
}

// Additional function.
// It toggles the expansion indicator ("+") on the header of table if at least one of additional rows is open.
function ToggleIndicator() {
    const tbodyIndicators = document.getElementsByClassName(styles.expansion_indicator);
    const theadIndicator = tbodyIndicators.item(0).closest('table').querySelector('th.md');

    let elementIsActive = false;
    let index;
    for(index = 0; index < tbodyIndicators.length; index++){
        tbodyIndicators.item(index).closest('tr').dataset.state == 'expanded' && [elementIsActive = true];
    }

    if(elementIsActive){
        theadIndicator.className = theadIndicator.className.replace('md-add', 'md-remove');
    }
    else{
        theadIndicator.className = theadIndicator.className.replace('md-remove', 'md-add');
    }
}

