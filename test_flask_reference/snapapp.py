#!flask/bin/python
import os.path
import json
import uuid

from flask import Flask, jsonify, abort, make_response, request, url_for


app = Flask(__name__)


# 404 error handler
@app.errorhandler(404)
def not_found(error):
    return make_response(jsonify({'error': 'Not found'}), 404)


# Get info from file
def read_file(file_name):
    data = []
    if os.path.isfile(file_name):
            print("file exists")
            with open(file_name, 'r') as data_file:
                for line in data_file:
                    data.append(json.loads(line))
    else:
        data_file = open(file_name, 'w')
    data_file.close()
    print ('read file ', data)
    return data


# Write info to file
def write_file(file_name, info):
    print("write file info: ", info)
    if os.path.isfile(file_name):
        with open(file_name, 'a') as data_file:
            #data = data_file.write(str(info))
            data = json.dump(info, data_file)
            data_file.write('\n')
            data_file.flush()
            data_file.close()
    else:
        with open(file_name, 'w') as data_file:
            #data = data_file.write(str(info))
            data = json.dump(info, data_file)
            data_file.write('\n')
            data_file.flush()
            data_file.close()
    return data


# snapshot creator
def make_public_snapshot(snapshot):
    new_snapshot = {}
    for field in snapshot:
        if field == 'id':
            new_snapshot['uri'] = url_for('get_snapshot', snapshot_id=snapshot['id'], _external=True)
        else:
            new_snapshot[field] = snapshot[field]
    return new_snapshot


# get Snapshot/Share data
snapshots = read_file('snapshots')
shares = read_file('shares')


# get all snapshots
@app.route('/todo/api/v1.0/snapshots', methods=['GET'])
def get_snapshots():
    return jsonify({'snapshots': [make_public_snapshot(snapshot) for snapshot in snapshots]})


# get an individual snapshot
@app.route('/todo/api/v1.0/snapshots/<int:snapshot_id>', methods=['GET'])
def get_snapshot(snapshot_id):
    snapshot = [snapshot for snapshot in snapshots if snapshot['id'] == snapshot_id]
    if len(snapshot) == 0:
        abort(404)
    return jsonify({'snapshot': [make_public_snapshot(snapshot) for snapshot in snapshots][0]})


# Post new snapshot
@app.route('/todo/api/v1.0/snapshots', methods=['POST'])
def create_snapshot():
    print("POST request: " + request.data)
    if not request.json or not 'shareID' in request.json:
        abort(400)
    guid = uuid.uuid1()
    if len(snapshots) > 0:
        snapshot = {
            'id': snapshots[-1]['id'] + 1,
            'snapshotID': str(guid),
            'shareID': int(request.json['shareID'])
        }
    else:
        snapshot = {
            'id': 1,
            'snapshotID': str(guid),
            'shareID': int(request.json['shareID'])
        }
    print(snapshot)
    snapshots.append(snapshot)
    write_file('snapshots', snapshot)
    return jsonify({'snapshot': make_public_snapshot(snapshot)}), 201


# share creator
def make_public_share(share):
    new_share = {}
    for field in share:
        if field == 'id':
            new_share['uri'] = url_for('get_share', share_id=share['id'], _external=True)
        else:
            new_share[field] = share[field]
    return new_share


# get all shares
@app.route('/todo/api/v1.0/shares', methods=['GET'])
def get_shares():
    return jsonify({'shares': [make_public_share(share) for share in shares]})


# get an individual share
@app.route('/todo/api/v1.0/shares/<int:share_id>', methods=['GET'])
def get_share(share_id):
    share = [share for share in shares if share['id'] == share_id]
    if len(share) == 0:
        abort(404)
    return jsonify({'share': [make_public_share(share) for share in shares][0]})


# Post new shares
@app.route('/todo/api/v1.0/shares', methods=['POST'])
def create_share():
    if len(shares) > 0:
        share = {
            'id': shares[-1]['id'] + 1,
            'shareID': shares[-1]['id'] + 1
        }
    else:
        share = {
            'id': 1,
            'shareID': 1
        }
    print(share)
    shares.append(share)
    write_file('shares', share)
    return jsonify({'share': make_public_share(share)}), 201


# main to run program
if __name__ == '__main__':
    app.run(host = '0.0.0.0')