This scounting app generates the Scounting Page from a JSon file that follows the same structure as the one used by Red Hawks Robotics.

The following field types- "type" element in the JSon file- are valid and behave as described below:
<ol>
  <li>select - will display a dropdown list. Reset will clear the selection</li>
  <li>text - will display a multiline text box. Reset will clear it's value</li>
  <li>number - will display a numerical text box. Reset will clear it's value</li>
  <li>boolean - will display a checkbox. Reset will uncheck it</li>
  <li>counter - will display a numerical text box with minus and plus buttons. Reset will set it to 0</li>
  <li>autoNumber - will display a numerical text box. Reset will increment its value by 1. Recommended for the Match Number field</li>
  <li>persistentText - will display a multiline text box. Reset will NOT clear it's value. Recommended for the Scouter Name field</li>
  <li>persistentList - will display a dropdown list. Reset will NOT clear the selection. Recommended for lists that don't change between matches, like the Event and Robot (Red1, Red2, etc)</li>
</ol>

<b>For lists that are required but have a default value that has no selection meaning, the value should be a blank "". See example below:</b>
<blockquote>
            "name": "Postmatch",</br>
            "fields": [</br>
                {</br>
                    "code": "ds",</br>
                    "title": "Driver Skill Rating from 1 (poor) to 10 (incredible)",</br>
                    "type": "select",</br>
                    "choices": {</br>
                      <b>"": "-",</b></br>
                        "1": "1",</br>
                        "2": "2",</br>
                        "3": "3",</br>
                        "4": "4",</br>
                        "5": "5",</br>
                        "6": "6",</br>
                        "7": "7",</br>
                        "8": "8",</br>
                        "9": "9",</br>
                        "10": "10"</br>
                    },</br>
                 <b>"defaultValue": "",</b></br>
                    "required": true</br>
                },</br>
</blockquote>
