import sys
from pathlib import Path


def populate(base: str, template: str, value: str) -> str:
	old = template.format('')
	new = template.format(value)

	return base.replace(old, new)


_, config, keystore, username, password = sys.argv

config = Path(config)
keystore = Path(keystore)

with config.open('r') as fp:
	config_text = fp.read()


replace_map = {
	'': keystore,
	'_user': username,
	'_password': password,
}

for suffix, value in replace_map.items():
	config_text = populate(config_text, f'keystore/debug{suffix}="{{}}"', value)
	config_text = populate(config_text, f'keystore/release{suffix}="{{}}"', value)

with config.open('w') as fp:
	fp.write(config_text)
